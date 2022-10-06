using Amazon.DynamoDBv2.DataModel;

namespace FollowerService.Contracts.Models
{
    [DynamoDBTable("followers")]
    public class Follower
    {
        [DynamoDBHashKey("userId")]
        public Guid UserId { get; set; }

        [DynamoDBRangeKey("followerId")]
        public Guid FollowerId { get; set; }
    }

}
