## Tree

In order to add functionality your custom section, you can start by adding a custom tree. 
You can create your own tree by creating a `CustomTreeController` derived from `TreeController`. 
The TreeControllers are used by Umbraco to generate trees for each section, and have two methods
which must be overridden. The TreeController is a bit of an oldy, and does not support anything 
`async`. This TreeController will probably be replaced by something better in Umbraco 8.x, but it
will to do for now. The most simplistic implementation of `Controllers/CustomTreeController.cs` will
look something like this:

``` Csharp
using System.Net.Http.Formatting;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Controllers
{
	[Tree("customSection", "customTree", "Custom Section", iconClosed: "icon-tree", iconOpen: "icon-trophy")]
	[PluginController("CustomSection")]
	public class CustomTreeController : TreeController
	{
		protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
		{
			return new TreeNodeCollection();
		}

		protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
		{
			return new MenuItemCollection();
		}
	}
}

```

The corresponding tree will look something like this:

![Tree](images/tree1.png)

There are two methods on the controller, `GetTreeNodes` and `GetMenuForNode`. The first method is for 
outputing tree nodes to build up the tree, the second one is for outputing menu items for the context menu
of each tree node.

