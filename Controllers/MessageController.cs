using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;

namespace TelegramBot.Controllers
{
    [Route(@"api/message/update")]
    public class MessageController : Controller
    {
        [HttpPost]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var client = await Bot.GetClient();
            Message message;
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    message = update.CallbackQuery.Message;
                    var chatId = message.Chat.Id;
                    switch (update.CallbackQuery.Data)
                    {
                        case "query":
                            // action
                            break;
                        case "first":
                            await client.SendTextMessageAsync(chatId, $"You press on first button!");
                            break;
                        case "second":
                            await client.SendTextMessageAsync(chatId, $"You press on second button!");
                            break;
                    }
                    break;
                case UpdateType.Message:
                    message = update.Message;
                    if (message.Text.Equals("/start"))
                    {
                        chatId = message.Chat.Id;
                        await client.SendTextMessageAsync(chatId, $"Добро пожаловать! Я {AppSettings.Name}!",
                            replyMarkup: new InlineKeyboardMarkup
                            (
                                new[]
                                {
                                    new[] { InlineKeyboardButton.WithCallbackData("First button", "first") },
                                    new[] { InlineKeyboardButton.WithCallbackData("Second button", "second") }
                                }
                            )
                        );
                    }
                    break;
            }
            return Ok();
        }
    }
}
