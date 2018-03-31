using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using UmbracoCustomSection.App_Plugins.CustomSection.Data;
using UmbracoCustomSection.App_Plugins.CustomSection.Models;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Controllers
{
    [PluginController("CustomSection")]
    public class SortController : UmbracoAuthorizedJsonController
    {
        private readonly CustomSectionDbContext _dbContext;

        public SortController(CustomSectionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<Node>> GetList(int id)
        {
            return await _dbContext.Nodes.Where(n => n.Id == id).SelectMany(n => n.SubNodes).ToListAsync();
        }
    }
}