using Amazon.SQS.Model;
using Amazon.SQS;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using FakeItEasy;
using Amazon;

namespace FollowerService.UnitTest
{
    public class IntegrationTest
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;
        public IntegrationTest()
        {
            _client = A.Fake<AmazonDynamoDBClient>();
            _context = A.Fake<DynamoDBContext>();
        }

        [Fact]
        
        public async Task SQSQueueExists()
        {
            var client = new AmazonSQSClient();

            var request = new GetQueueUrlRequest
            {
                QueueName = "FollowerTimelineQueue",

            };

            var response = client.GetQueueUrlAsync(request);
            Assert.NotNull(response);
        }
        [Fact]
        public async Task SQSAccessIdTest()
        {
            var client = new AmazonSQSClient();

            var request = new GetQueueUrlRequest
            {
                QueueOwnerAWSAccountId = "AKIARDAVOLDTQ2UHSSQQ"
            };

            var response = client.GetQueueUrlAsync(request);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task DynamoDBTest()
        {
            var _client = new AmazonDynamoDBClient(RegionEndpoint.EUCentral1);



            Assert.Equal(_client.Config.RegionEndpoint, RegionEndpoint.EUCentral1);
            Assert.Equal(_client.Config.AuthenticationServiceName, "dynamodb");
        }


    }
}
