using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Web;
using UmbracoCustomSection.App_Plugins.CustomSection.Models;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Data
{
    public class CustomSectionDbContext : DbContext
    {
        public CustomSectionDbContext(DbContextOptions<CustomSectionDbContext> options) : base(options)
        {

        }

        public DbSet<Node> Nodes { get; set; }
    }
}