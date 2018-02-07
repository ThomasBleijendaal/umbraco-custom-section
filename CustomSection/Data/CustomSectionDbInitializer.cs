using System.Collections.Generic;
using System.Linq;
using UmbracoCustomSection.App_Plugins.CustomSection.Models;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Data
{
    public static class CustomSectionDbInitializer
    {
        static bool inited = false;

        public static void Initialize(CustomSectionDbContext customSectionDbContext)
        {
            customSectionDbContext.Nodes.AddRange(new[]
            {
                new Node { Id= 1, Name = "Item 1", Color = "green", SubNodes = new List<Node>
                {
                    new Node { Id = 2, Name = "Item 11", Color = "green", SubNodes = new List<Node>
                    {
                        new Node { Id = 3, Name = "Item 111", Color = "green" },
                        new Node { Id = 4, Name = "Item 112", Color = "yellow" },
                        new Node { Id = 5, Name = "Item 113", Color = "red" }
                    }},
                    new Node { Id = 6, Name = "Item 12", Color = "yellow", SubNodes = new List<Node>
                    {
                        new Node { Id = 7, Name = "Item 121", Color = "green" },
                        new Node { Id = 8, Name = "Item 122", Color = "yellow" },
                        new Node { Id = 9, Name = "Item 123", Color = "red" }
                    }},
                    new Node { Id = 10, Name = "Item 13", Color = "red", SubNodes = new List<Node>
                    {
                        new Node { Id = 11, Name = "Item 131", Color = "green" },
                        new Node { Id = 12, Name = "Item 132", Color = "yellow" },
                        new Node { Id = 13, Name = "Item 133", Color = "red" }
                    }}
                }},
                new Node { Id= 14, Name = "Item 2", Color = "yellow", SubNodes = new List<Node>
                {
                    new Node { Id = 15, Name = "Item 21", Color = "green", SubNodes = new List<Node>
                    {
                        new Node { Id = 16, Name = "Item 211", Color = "green" },
                        new Node { Id = 17, Name = "Item 212", Color = "yellow" },
                        new Node { Id = 18, Name = "Item 213", Color = "red" }
                    }},
                    new Node { Id = 19, Name = "Item 22", Color = "yellow", SubNodes = new List<Node>
                    {
                        new Node { Id = 20, Name = "Item 221", Color = "green" },
                        new Node { Id = 21, Name = "Item 222", Color = "yellow" },
                        new Node { Id = 22, Name = "Item 223", Color = "red" }
                    }},
                    new Node { Id = 23, Name = "Item 23", Color = "red", SubNodes = new List<Node>
                    {
                        new Node { Id = 24, Name = "Item 231", Color = "green" },
                        new Node { Id = 25, Name = "Item 232", Color = "yellow" },
                        new Node { Id = 26, Name = "Item 233", Color = "red" }
                    }}
                }},
                new Node { Id= 27, Name = "Item 3", Color = "red", SubNodes = new List<Node>
                {
                    new Node { Id = 28, Name = "Item 31", Color = "green", SubNodes = new List<Node>
                    {
                        new Node { Id = 29, Name = "Item 311", Color = "green" },
                        new Node { Id = 30, Name = "Item 312", Color = "yellow" },
                        new Node { Id = 31, Name = "Item 313", Color = "red" }
                    }},
                    new Node { Id = 32, Name = "Item 32", Color = "yellow", SubNodes = new List<Node>
                    {
                        new Node { Id = 33, Name = "Item 321", Color = "green" },
                        new Node { Id = 34, Name = "Item 322", Color = "yellow" },
                        new Node { Id = 35, Name = "Item 323", Color = "red" }
                    }},
                    new Node { Id = 36, Name = "Item 33", Color = "red", SubNodes = new List<Node>
                    {
                        new Node { Id = 37, Name = "Item 331", Color = "green" },
                        new Node { Id = 38, Name = "Item 332", Color = "yellow" },
                        new Node { Id = 39, Name = "Item 333", Color = "red" }
                    }}
                }}
            });

            customSectionDbContext.SaveChanges();
        }
    }
}