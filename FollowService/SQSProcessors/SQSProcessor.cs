using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using FollowerService.Contracts.Models;
using FollowerService.Interfaces;
using System.Text.Json;

namespace FollowerService.SQSProcessors
{

    public class SQSProcessor : ISQSProcessor //: BackgroundProcessor
    {

        private IConfiguration configuration;

        public SQSProcessor()
        {
        }

        public SQSProcessor(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task SQSPost(FollowerInputModel followerRequest)
        {
            var sqsPostQueue = Environment.GetEnvironmentVariable("AWS_FOLLOWER_SQS_QUEUE");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

            var request = new SendMessageRequest()
            {
                QueueUrl = sqsPostQueue,
                MessageBody = JsonSerializer.Serialize(followerRequest) + "add"
            };
            _ = await client.SendMessageAsync(request);
        }
        public async Task SQSRemove(FollowerInputModel followerRequest)
        {
            var sqsPostQueue = Environment.GetEnvironmentVariable("AWS_FOLLOWER_SQS_QUEUE");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

            var request = new SendMessageRequest()
            {
                QueueUrl = sqsPostQueue,
                MessageBody = JsonSerializer.Serialize(followerRequest) + "remove"
            };
            _ = await client.SendMessageAsync(request);
        }
    }
}