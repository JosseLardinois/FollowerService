using Amazon.SQS.Model;
using System.Linq;

namespace FollowerService.Contract.SQSProcessors
{
    public class MessageProcessor
    {
        public Message addToMessageAndDisplay(Message message)
        {
            string addtostring = "added to body";

            message.Body += addtostring;
            Console.WriteLine(message.Body);
            return message;
        }
    }
}
