using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TweetSharp;

namespace FirstBot
{
    class Program
    {
        private static string customer_key = "pNFWvKIirO3EJ8SXssLqMjTLO";
        private static string customer_key_secret = "V1QLmPAfVv3y4PxOSEPoL6sqIFWaKyGX2JBUBgZSx04uCyC6dA";
        private static string access_token = "1126130105397522432-HnalIHxLz7DHJVUmD0PBxxjHsvLb16";
        private static string access_toten_secret = "feGVexsvaukgvsdovSy0HKziX43pIC86ZHk5Csw5x7A0H";

        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_toten_secret);

        private static int currentImageID = 0;
        private static List<string> imageList = new List<string> { $"Z:/Images/avengers-endgame.jpg"};
        static void Main(string[] args)
        {
            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            String Input = Console.ReadLine();
            SendMediaTweet("Test Tweet ", currentImageID);
            Console.Read();
            
        }
        private static void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
             {
                 if (response.StatusCode == System.Net.HttpStatusCode.OK)
                 {
                     Console.ForegroundColor = ConsoleColor.Green;
                     Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                     Console.ResetColor();
                 }
                 else
                 {
                     Console.ForegroundColor = ConsoleColor.Red;
                     Console.WriteLine($"<ERROR> " + response.Error.Message);
                     Console.ResetColor();
                 }
             });
        }
        private static void SendMediaTweet(string _status, int imageID)
        {
            using (var stream = new FileStream(imageList[imageID], FileMode.Open))
            {
                service.SendTweetWithMedia(new SendTweetWithMediaOptions
                {
                    Status = _status,
                    Images = new Dictionary<string, Stream> { { imageList[imageID], stream } }
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                Console.ResetColor();

                if ((currentImageID + 1) == imageList.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("<BOT> - End of Image Array");
                    Console.ResetColor();
                }
                else 
                {
                    currentImageID++;
                }

            }
        }
    }
}
