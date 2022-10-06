﻿using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using FollowerService.Contracts.Models;
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
        public async Task SQSPost(FollowerInputModel followerRequest)
        {
            var sqsPostQueue = Environment.GetEnvironmentVariable("AWS_POST_SQS_QUEUE");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

            var request = new SendMessageRequest()
            {
                QueueUrl = sqsPostQueue,
                MessageBody = JsonSerializer.Serialize(followerRequest)
            };
            _ = await client.SendMessageAsync(request);
        }
    }
}