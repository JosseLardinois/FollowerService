using Amazon.Runtime;
using Amazon.SQS.Model;
using Amazon.SQS;
using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Models;
using FollowerService.SQSProcessors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Amazon;

namespace FollowerService2.Controllers
{
    [Route("[controller]")]
    public class FollowersController : Controller
    {
        private IFollowersRepository _repository;
        private IConfiguration _configuration;


        public FollowersController(IFollowersRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        
        

        [HttpGet]
        public async Task<IEnumerable<Follower>> GetAllFollowers(Guid userId)
        {
            return await _repository.All(userId);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(FollowerInputModel model)
        {
            await _repository.Add(model);
            SQSProcessor processor = new SQSProcessor(_configuration);
            await processor.SQSPost(model);
            return Ok(model);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(FollowerInputModel follower)
        {
            await _repository.Remove(follower);
            return NoContent();
        }
    }
}