using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using FollowerService.Contract.Models;
using System.Text.Json;

namespace FollowerService.SQSProcessors
{

    public class SQSProcessor //: BackgroundProcessor
    {

        private IConfiguration configuration;

        public SQSProcessor(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    var appconfig = configuration.GetSection("AppConfig").Get<AppConfig>();
        //    Console.WriteLine("Starting background processor");
        //    var credentials = new BasicAWSCredentials(appconfig.AccessKeyId, appconfig.SecretAccessKey);
        //    var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        Console.WriteLine($"Getting messages from the queue {DateTime.Now}");
        //        var request = new ReceiveMessageRequest()
        //        {
        //            QueueUrl = appconfig.QueueUrl,
        //            WaitTimeSeconds = 15,
        //            VisibilityTimeout = 20//for long polling

        //        };
        //        var response = await client.ReceiveMessageAsync(request);
        //        foreach (var message in response.Messages)
        //        {
        //            Console.WriteLine(message.Body);
        //            if (message.Body.Contains("RequestFollowersFromTimeline"))
        //            {
        //                MessageProcessor messageProcessor = new MessageProcessor(); 
        //                var processedMessage = messageProcessor.addToMessageAndDisplay(message);
        //                Console.WriteLine("ProcessedMessage " + processedMessage.Body );//gain the nessecary information and call a producer to send the according information
        //            };

        //                 // If body contains request follwowers give according followers
        //            //send to dead letter queue if message contains exception
        //            //call createmethod and put message body inside 

        //            await client.DeleteMessageAsync("https://sqs.eu-central-1.amazonaws.com/075206908135/PostTimelineQueue", message.ReceiptHandle);
        //        }
        //    }
        //}
        public async Task SQSPost(Follower followerRequest)
        {
            var appconfig = configuration.GetSection("AppConfig").Get<AppConfig>();
            var credentials = new BasicAWSCredentials(appconfig.AccessKeyId, appconfig.SecretAccessKey);
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

            var request = new SendMessageRequest()
            {
                QueueUrl = appconfig.QueueUrl,
                MessageBody = JsonSerializer.Serialize(followerRequest)
            };
            _ = await client.SendMessageAsync(request);
        }
    }
}