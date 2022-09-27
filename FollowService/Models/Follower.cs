using Amazon.DynamoDBv2.DataModel;

namespace FollowerService.Models
{
    [DynamoDBTable("followers")]
    public class Follower
    {
        [DynamoDBHashKey("userId")]
        public string? UserId { get; set; }

        [DynamoDBRangeKey("followerId")]
        public string? FollowerId { get; set; }
        
    }
}