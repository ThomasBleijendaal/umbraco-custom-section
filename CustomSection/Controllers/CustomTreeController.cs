using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Search;
using Umbraco.Web.Trees;
using UmbracoCustomSection.App_Plugins.CustomSection.Data;
using UmbracoCustomSection.App_Plugins.CustomSection.Models;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Controllers
{
    [Tree("customSection", "customTree", "Custom Section", iconClosed: "icon-tree", iconOpen: "icon-trophy")]
    [SearchableTree("searchResultFormatter", "configureContentResult")]
    [PluginController("CustomSection")]
    public class CustomTreeController : TreeController, ISearchableTree
    {
        private readonly CustomSectionDbContext _dbContext;

        public CustomTreeController(CustomSectionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var collection = new TreeNodeCollection();

            if (int.TryParse(id, out int parentNodeId))
            {
                var nodes = (id == "-1")
                    ? _dbContext.Nodes.Where(n => n.ParentNode == null).ToList()
                    : _dbContext.Nodes.Where(n => n.ParentNode.Id == parentNodeId).ToList();

                collection.AddRange(nodes.Select(node =>
                    CreateTreeNode(
                        $"{node.Id}",
                        $"{parentNodeId}",
                        queryStrings,
                        node.Name,
                        GetIconForNode(node),
                        node.SubNodes?.Any() ?? false)));
            }

            return collection;
        }

        private string GetIconForNode(Node node)
        {
            if (node.ParentNode == null)
            {
                return $"icon-tree color-{node.Color}";
            }
            else if (node.SubNodes?.Any() ?? false)
            {
                return $"icon-trophy color-{node.Color}";
            }
            else
            {
                return $"icon-stream color-{node.Color}";
            }
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var collection = new MenuItemCollection();

            if (id == "-1")
            {
                var item = new MenuItem("edit", "Edit");
                item.NavigateToRoute("/some/route");
                collection.Items.Add(item);
            }
            else if (id.Length == 1)
            {
                collection.Items.Add<ActionSort>("Custom Sort").LaunchDialogView("/App_Plugins/CustomSection/backoffice/customTree/dialog.html", "Custom Dialog");
            }
            else if (id.Length == 2)
            {
                collection.Items.Add<ActionNew>("Create").NavigateToRoute("/customSection/customTree/customPage/edit");
            }

            collection.Items.Add<ActionRefresh>("Reload", true);

            return collection;
        }

        public IEnumerable<SearchResultItem> Search(string query, int pageSize, long pageIndex, out long totalFound, string searchFrom = null)
        {
            totalFound = 1;

            var results = new List<SearchResultItem>();

            var item = new SearchResultItem
            {
                Icon = "icon-tree",
                Id = "1",
                Name = query,
                ParentId = -1,
                Path = $"-1,1",
                Score = 0.5f
            };
            item.AdditionalData.Add("Url", "/some/custom/url");

            results.Add(item);

            return results;
        }
    }
}