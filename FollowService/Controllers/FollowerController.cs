using FollowerService.Contract.Interfaces;
using FollowerService.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace FollowerService2.Controllers
{
    [Route("[controller]")]
    public class FollowersController : Controller
    {
        private IFollowersRepository _repository;

        public FollowersController(IFollowersRepository repository)
        {
            _repository = repository;
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