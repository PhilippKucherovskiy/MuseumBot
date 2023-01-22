using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;

namespace MuseumBot
{
    class Program
    {
        private static TelegramBotClient _bot;
        private static int _currentPage = 0;
        private static string[] _pages = { "page1.jpg", "page2.jpg", "page3.jpg" };
        private static string[] _captions = { "Caption 1", "Caption 2", "Caption 3" };
        private static string[] _texts = { "Text 1", "Text 2", "Text 3" };

        static void Main(string[] args)
        {
            _bot = new TelegramBotClient("5981544709:AAGjFu0jUn0jrYWHeR3wq4BDCmLOQus1VAo");
            _bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            _bot.StartReceiving();
            Console.ReadLine();
            _bot.StopReceiving();
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            var buttonText = e.CallbackQuery.Data;
            var chatId = e.CallbackQuery.Message.Chat.Id;

            if (buttonText == "Start Tour")
            {
                await StartTour(chatId);
            }
            else if (buttonText == "Next")
            {
                await SendNextPage(chatId);
            }
        }
        private static async Task StartTour(long chatId)
        {
            // Send initial message with a button to start the tour
            var keyboard = new InlineKeyboardMarkup(new[]
            {
            new [] // first row
            {
                InlineKeyboardButton.WithCallbackData("Start Tour")
            },
        });
            await _bot.SendMessageAsync(chatId, "Добро пожаловать на обзорную экскурсию по музею!", replyMarkup: keyboard);
        }

        private static async Task SendNextPage(long chatId)
        {
            // Send next page of the tour
            if (_currentPage < _pages.Length)
            {
                await _bot.SendPhotoAsync(chatId, _pages[_currentPage], _captions[_currentPage]);
                await _bot.SendMessageAsync(chatId, _texts[_currentPage]);
                _currentPage++;
                if (_currentPage < _pages.Length)
                {
                    await _bot.SendMessageAsync(chatId, "Next", replyMarkup: new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Next")
                    }
                }));
                }
            }
            else
            {
                await _bot.SendMessageAsync(chatId, "Tour has ended, thank you for visiting!");
                _currentPage = 0;
            }
        }
    }
