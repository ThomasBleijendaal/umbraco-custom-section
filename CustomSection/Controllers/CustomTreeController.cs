using System.Collections.Generic;
using System.Net.Http.Formatting;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Search;
using Umbraco.Web.Trees;
using UmbracoCustomSection.App_Plugins.CustomSection.Data;

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

            if (id == "-1")
            {
                collection.Add(CreateTreeNode("A", "-1", queryStrings, "Item A", "icon-tree color-green", true));
                collection.Add(CreateTreeNode("B", "-1", queryStrings, "Item B", "icon-tree color-yellow", true));
                collection.Add(CreateTreeNode("C", "-1", queryStrings, "Item C", "icon-tree color-red", true));
            }
            else if (id.Length == 1)
            {
                collection.Add(CreateTreeNode($"{id}1", id, queryStrings, $"Item {id}1", "icon-trophy color-green", true));
                collection.Add(CreateTreeNode($"{id}2", id, queryStrings, $"Item {id}2", "icon-trophy color-yellow", true));
                collection.Add(CreateTreeNode($"{id}3", id, queryStrings, $"Item {id}3", "icon-trophy color-red", true));
            }
            else if (id.Length == 2)
            {
                collection.Add(CreateTreeNode($"{id}1", id, queryStrings, $"Item {id}a", "icon-stream color-green", false));
                collection.Add(CreateTreeNode($"{id}2", id, queryStrings, $"Item {id}b", "icon-stream color-yellow", false));
                collection.Add(CreateTreeNode($"{id}3", id, queryStrings, $"Item {id}c", "icon-stream color-red", false));
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