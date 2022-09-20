using Amazon.DynamoDBv2.DataModel;

namespace FollowerService.Models
{
    [DynamoDBTable("followers")]
    public class Follower
    {
        [DynamoDBHashKey("id")]
        public Guid Id { get; set; }
        [DynamoDBProperty("userId")]
        public int? UserId { get; set; }

        [DynamoDBProperty("followerUserId")]
        public List<int>? FollowerUserId { get; set; }
        
    }
}