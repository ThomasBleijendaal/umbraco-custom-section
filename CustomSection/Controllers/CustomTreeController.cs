using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Mvc;
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
    [SearchableTree("customResultFormatter", "configureResult")]
    [PluginController("CustomSection")]
    public class CustomTreeController : TreeController, ISearchableTree
    {
        private readonly CustomSectionDbContext _dbContext;

        public CustomTreeController()
        {
            _dbContext = DependencyResolver.Current.GetService<CustomSectionDbContext>();
        }

        public CustomTreeController(CustomSectionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var collection = new TreeNodeCollection();

            if (int.TryParse(id, out var parentNodeId))
            {
                var nodes = (id == "-1")
                    ? _dbContext.Nodes.Include(n => n.SubNodes).Where(n => n.ParentNode == null).ToList()
                    : _dbContext.Nodes.Include(n => n.SubNodes).Where(n => n.ParentNode.Id == parentNodeId).ToList();

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

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var collection = new MenuItemCollection();

            if (id == "-1")
            {
                var item = new MenuItem("edit", "Edit");
                item.NavigateToRoute("/some/route");
                collection.Items.Add(item);
            }
            else if (int.TryParse(id, out var nodeId))
            {
                var node = _dbContext.Nodes.Include(n => n.ParentNode).Include(n => n.SubNodes).First(n => n.Id == nodeId);

                if(node.ParentNode == null)
                {
                    collection.Items.Add<ActionSort>("Custom Sort").LaunchDialogView("/App_Plugins/CustomSection/backoffice/dialogs/sort.html", "Custom Dialog");
                }
                else
                {
                    collection.Items.Add<ActionNew>("Create").NavigateToRoute("/customSection/customTree/customPage/edit");
                }
            }

            collection.Items.Add<ActionRefresh>("Reload", true);

            return collection;
        }

        public IEnumerable<SearchResultItem> Search(string query, int pageSize, long pageIndex, out long totalFound, string searchFrom = null)
        {
            var results = _dbContext.Nodes.Where(n => n.Name.ToLower().Contains(query.ToLower())).ToList();

            totalFound = results.Count;

            return results.Select(node =>
            {
                var item = new SearchResultItem
                {
                    Alias = $"{node.Name} alias",
                    Icon = GetIconForNode(node),
                    Id = node.Id,
                    Name = node.Name,
                    ParentId = node.ParentNode?.Id ?? -1,
                    Path = GetPathForNode(node),
                    Score = node.Name.Intersect(query).Count() / (float)node.Name.Length
                };
                item.AdditionalData.Add("Url", "/some/path");

                return item;
            });
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

        private string GetPathForNode(Node node)
        {
            return (node.ParentNode != null)
                ? $"{GetPathForNode(node.ParentNode)},{node.Id}"
                : $"{node.Id}";
        }
    }
}