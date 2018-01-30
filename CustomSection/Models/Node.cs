using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Models
{
    public class Node
    {
        public Node ParentNode { get; set; }
        public IList<Node> SubNodes { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}