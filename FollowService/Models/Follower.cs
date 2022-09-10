using Amazon.DynamoDBv2.DataModel;

namespace FollowerService.Models
{
    [DynamoDBTable("follower")]
    public class Follower
    {
        [DynamoDBHashKey("id")]
        public int? Id { get; set; }

        [DynamoDBProperty("userId")]
        public int? UserId { get; set; }

        [DynamoDBProperty("followerUserId")]
        public int? FollowerUserId { get; set; }

    }
}