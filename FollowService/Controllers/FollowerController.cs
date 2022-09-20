using Amazon.DynamoDBv2.DataModel;
using FollowerService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamoStudentManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowerController : ControllerBase
{
    private readonly IDynamoDBContext _context;

    public FollowerController(IDynamoDBContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetById(int userId)
    {
        var user = await _context.LoadAsync<Follower>(userId);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFollowers()
    {
        var followers = await _context.ScanAsync<Follower>(default).GetRemainingAsync();
        return Ok(followers);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFollower(Follower followerRequest)
    {
        followerRequest.Id = Guid.NewGuid();
        var follower = await _context.LoadAsync<Follower>(followerRequest.Id);
        if (follower != null) return BadRequest($"Follower with Id {followerRequest.Id} Already Exists");
        await _context.SaveAsync(followerRequest);
        return Ok(followerRequest);
    }

    [HttpDelete("{followerId}")]
    public async Task<IActionResult> DeleteFollower(int followerId)
    {
        var follower = await _context.LoadAsync<Follower>(followerId);
        if (follower == null) return NotFound();
        await _context.DeleteAsync(follower);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFollower(Follower followerRequest)
    {
        var follower = await _context.LoadAsync<Follower>(followerRequest.Id);
        if (follower == null) return NotFound();
        await _context.SaveAsync(followerRequest);
        return Ok(followerRequest);
    }
}