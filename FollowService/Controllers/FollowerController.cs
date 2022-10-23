using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using FollowerService.Interfaces;
using System.Net;
using FollowerService.Exceptions;

namespace FollowerService.Controllers
{
    [Route("[controller]")]
    public class FollowersController : Controller
    {
        private IFollowersRepository _repository;
        private IConfiguration _configuration;
        private ISQSProcessor _processor;


        public FollowersController(IFollowersRepository repository, IConfiguration configuration, ISQSProcessor sqsProcessor)
        {
            _repository = repository;
            _configuration = configuration;
            _processor = sqsProcessor;
        }
        
        

        [HttpGet]
        public async Task<IEnumerable<Follower>> GetAllFollowers(Guid userId)
        {
            try
            {
                return await _repository.All(userId);
            }
            catch (ProfileNotFoundException e)
            {
               return new List<Follower>();
            }

        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("Create")]
        public async Task<ActionResult<Guid>> Create([FromBody] FollowerInputModel model)//ActionResult needs to change
        {
            try
            {
                var follower = await _repository.Add(model);
                _ = _processor.SQSPost(model);
                return Ok(follower);
            }
            catch (DuplicateWaitObjectException e)
            {
                return Conflict(e.Message);
            }

        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(FollowerInputModel follower)
        {
            try
            {
                await _repository.Remove(follower);
                await _processor.SQSRemove(follower);
                return NoContent();
            }
            catch (ProfileNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }
    }
}