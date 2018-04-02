using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoCustomSection.App_Plugins.CustomSection.ViewModels
{
    public class NodeViewModel
    {
        public NodeViewModel ParentNode { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}