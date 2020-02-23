using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot.Models
{
    public class Bot
    {
        private static TelegramBotClient client;
        public static async Task<TelegramBotClient> GetClient()
        {
            if (client != null)
            {
                return client;
            }

            client = new TelegramBotClient(AppSettings.Key);
            var hook = string.Format(AppSettings.Url, "api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }
    }
}
