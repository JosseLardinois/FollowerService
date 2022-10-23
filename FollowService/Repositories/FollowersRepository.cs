using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime.CredentialManagement.Internal;
using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Models;
using FollowerService.SQSProcessors;

namespace FollowerService.Contracts.Repositories
{
    public class FollowersRepository : IFollowersRepository
    {
        
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public FollowersRepository()
        {

            _client = new AmazonDynamoDBClient(RegionEndpoint.EUCentral1);
            _context = new DynamoDBContext(_client);

        }

        public async Task<Follower> Add(FollowerInputModel entity) //public async Task<Follower> Add(FollowerInputModel entity)
        {
            var follower = new Follower
            {
                UserId = entity.UserId,
                FollowerId = entity.FollowerId,
            };
            
             await _context.SaveAsync(follower);
             return follower;
        }

        public async Task<IEnumerable<Follower>> All(Guid userId)
        {
            return await _context.QueryAsync<Follower>(userId).GetRemainingAsync();
        }

        public async Task Remove(FollowerInputModel entity)
        {
            var follower = new Follower
            {
                UserId = entity.UserId,
                FollowerId = entity.FollowerId,
            };
            var _follower = await _context.LoadAsync<Follower>(follower);
            await _context.DeleteAsync(_follower);
        }

        public async Task<Follower> Single(Guid followerId)
        {
            return await _context.LoadAsync<Follower>(followerId); //.QueryAsync<Follower>(FollowerId.ToString()).GetRemainingAsync();
        }
    }
}