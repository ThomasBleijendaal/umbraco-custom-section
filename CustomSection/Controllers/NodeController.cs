using ExpressMapper.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using UmbracoCustomSection.App_Plugins.CustomSection.Data;
using UmbracoCustomSection.App_Plugins.CustomSection.Models;
using UmbracoCustomSection.App_Plugins.CustomSection.ViewModels;

namespace UmbracoCustomSection.App_Plugins.CustomSection.Controllers
{
    [PluginController("CustomSection")]
    public class NodeController : UmbracoAuthorizedJsonController
    {
        private readonly CustomSectionDbContext _dbContext;

        public NodeController(CustomSectionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<NodeViewModel> GetNode(int id)
        {
            // TODO: this only fetches parent Nodes two levels up
            var node = await _dbContext.Nodes
                .Include(n => n.ParentNode).ThenInclude(n => n.ParentNode)
                .FirstOrDefaultAsync(n => n.Id == id);

            return node.Map<Node, NodeViewModel>();
        }

        [HttpPost]
        public async Task SaveNode(NodeViewModel model)
        {
            var node = await _dbContext.Nodes.FirstAsync(n => n.Id == model.Id);

            node.Name = model.Name;

            _dbContext.Update(node);
            await _dbContext.SaveChangesAsync();
        }

        [HttpPost]
        public async Task<NodeViewModel> CreateNode(NodeViewModel model)
        {
            var parentNode = await _dbContext.Nodes.FirstAsync(n => n.Id == model.ParentNode.Id);

            var node = new Node
            {
                Id = await _dbContext.Nodes.CountAsync() + 1,
                Name = model.Name,
                ParentNode = parentNode,
                Color = "black"
            };

            _dbContext.Add(node);
            await _dbContext.SaveChangesAsync();

            return await GetNode(node.Id);
        }
    }
}