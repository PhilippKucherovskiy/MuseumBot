using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using System.Xml.Serialization;
using System.IO;

namespace MuseumBot
{
    class Program
    {
        private static TelegramBotClient _bot;
        private static List<User> _users;
        private static XmlSerializer _serializer;
        private static string _fileName = "users.xml";

        static void Main(string[] args)
        {
            _bot = new TelegramBotClient("YOUR_TOKEN_HERE");
            _bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            _serializer = new XmlSerializer(typeof(List<User>));

            // Deserialize from XML file
            using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                _users = (List<User>)_serializer.Deserialize(fs);
            }

            _bot.StartReceiving();
            Console.ReadLine();
            _bot.StopReceiving();
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            long chatId = e.CallbackQuery.Message.Chat.Id;

            switch (buttonText)
            {
                case "Tour":
                    await SendTour(chatId);
                    break;
                case "Sign up":
                    await SignUp(chatId);
                    break;
                case "Feedback":
                    await Feedback(chatId);
                    break;
            }
        }

        private static async Task SendTour(long chatId)
        {
            // Send images and text for the tour
        }

        private static async Task SignUp(long chatId)
        {
            // Ask for user details
            // Add user to the list
            // Serialize to XML file
            var user = new User();
            // ask user for details
            _users.Add(user);
            using (FileStream fs = new FileStream(_fileName, FileMode.Create))
            {
                _serializer.Serialize(fs, _users);
            }
        }

        private static async Task Feedback(long chatId)
        {
            // Ask for feedback
        }
    }

    public class User
    {
        // Add properties for user details
    }
}
