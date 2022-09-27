using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Amazon.SQS.Model;
using Amazon.SQS;
using FollowerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Amazon;
using FollowerService.Processors;

namespace FollowerService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowerController : ControllerBase
{
    private readonly IDynamoDBContext _context; //lets us use the DynamoDB
    private readonly IConfiguration _configuration;

    public FollowerController(IDynamoDBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration; 
    }

    [HttpGet]
    public async Task<IEnumerable<Follower>> GetAllFollowers(string userId)
    {
        var i = await _context.QueryAsync<Follower>(userId).GetRemainingAsync();
        return i;
    }


    [HttpPost]
    public async Task<IActionResult> AddFollower(Follower follower)
    {
        await _context.SaveAsync(follower);
        SQSProcessor sqsProcessor = new SQSProcessor(_configuration);
        await sqsProcessor.SQSPost(follower);
        //await SQSPost(follower);
        return Ok(follower);
        
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteFollower(Follower follower)
    {
        var _follower = await _context.LoadAsync<Follower>(follower);
        if (_follower == null) return NotFound();
        await _context.DeleteAsync(_follower);
        return NoContent();
    }


}