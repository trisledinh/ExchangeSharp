using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeSharp.TelegramBot
{
	public class TelegramBot
	{
		static ITelegramBotClient botClient;
		static string accessToken = System.Configuration.ConfigurationManager.AppSettings["TelegramAccessToken"];
		static string defaultChatId = System.Configuration.ConfigurationManager.AppSettings["ChatId"];
		public static decimal minRatePercent = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["MinPercentRate"]);
		public TelegramBot()
		{
			botClient = new TelegramBotClient(accessToken);
			var me = botClient.GetMeAsync().Result;

			botClient.OnMessage += Bot_OnMessage;
			botClient.OnCallbackQuery += BotOnCallbackQueryReceived;
			botClient.OnInlineQuery += BotOnInlineQueryReceived;
			botClient.OnInlineResultChosen += BotOnChosenInlineResultReceived;

			botClient.StartReceiving(Array.Empty<UpdateType>());
		}

		static async void Bot_OnMessage(object sender, MessageEventArgs e)
		{

			var message = e.Message;
			if (message == null || message.Type != MessageType.Text)
				return;

			string[] arrMessage = message.Text.Split(' ');

			string symbol = arrMessage[1];
			decimal usdtRate = decimal.Parse(arrMessage[2]);
			decimal krwRate = decimal.Parse(arrMessage[3]);


			switch (arrMessage.First())
			{
				// Send inline keyboard
				case "/request":
					await SendDifferentRate(message, symbol, usdtRate, krwRate);
					break;

				// send custom keyboard
				case "/keyboard":
					await SendReplyKeyboard(message);
					break;

				//// send a photo
				//case "/photo":
				//	await SendDocument(message);
				//	break;

				//// request location or contact
				//case "/request":
				//	await RequestContactAndLocation(message);
				//	break;

				default:
					await Usage(message);
					break;
			}

		}

		static async Task SendDifferentRate(Message message, string symbol, decimal? usdtRate, decimal? krwRate)
		{
			await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

			// Simulate longer running task
			await Task.Delay(500);

			var inlineKeyboard = new InlineKeyboardMarkup(new[]
			{
                    // first row
                    new []
					{
						InlineKeyboardButton.WithCallbackData("BNB", "BNB"),
						InlineKeyboardButton.WithCallbackData("HOUBI", "HOUBI"),
					},
                    // second row
                    new []
					{
						InlineKeyboardButton.WithCallbackData("KUCOIN", "KUCOIN"),
						InlineKeyboardButton.WithCallbackData("OKEX", "OKEX"),
					}
				});
			await botClient.SendTextMessageAsync(
				chatId: message.Chat.Id,
				text: "Choose",
				replyMarkup: inlineKeyboard
			);
		}

		static async Task SendReplyKeyboard(Message message)
		{
			var replyKeyboardMarkup = new ReplyKeyboardMarkup(
				new KeyboardButton[][]
				{
						new KeyboardButton[] { "1.1", "1.2" },
						new KeyboardButton[] { "2.1", "2.2" },
				},
				resizeKeyboard: true
			);

			await botClient.SendTextMessageAsync(
				chatId: message.Chat.Id,
				text: "Choose",
				replyMarkup: replyKeyboardMarkup

			);
		}

		static async Task Usage(Message message)
		{
			const string usage = "Usage:\n" +
									"/inline   - send inline keyboard\n" +
									"/keyboard - send custom keyboard\n" +
									"/photo    - send a photo\n" +
									"/request  - request location or contact";
			await botClient.SendTextMessageAsync(
				chatId: message.Chat.Id,
				text: usage,
				replyMarkup: new ReplyKeyboardRemove()
			);
		}

		// Process Inline Keyboard callback data
		private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
		{
			var callbackQuery = callbackQueryEventArgs.CallbackQuery;

			await botClient.AnswerCallbackQueryAsync(
				callbackQueryId: callbackQuery.Id,
				text: $"Received {callbackQuery.Data}"
			);

			await botClient.SendTextMessageAsync(
				chatId: callbackQuery.Message.Chat.Id,
				text: $"Received {callbackQuery.Data}"
			);
		}

		#region Inline Mode

		private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
		{
			Console.WriteLine($"Received inline query from: {inlineQueryEventArgs.InlineQuery.From.Id}");

			InlineQueryResultBase[] results = {
                // displayed result
                new InlineQueryResultArticle(
					id: "3",
					title: "TgBots",
					inputMessageContent: new InputTextMessageContent(
						"hello"
					)
				)
			};
			await botClient.AnswerInlineQueryAsync(
				inlineQueryId: inlineQueryEventArgs.InlineQuery.Id,
				results: results,
				isPersonal: true,
				cacheTime: 0
			);
		}

		private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
		{
			Console.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
		}

		#endregion


		public static void SendMessageToChannel(string messageId, string? chatId)
		{
			if (string.IsNullOrEmpty(chatId))
			{
				chatId = defaultChatId;
			}

			botClient.SendTextMessageAsync(
				  chatId: chatId,
				  text: messageId
				);
		}
	}
}
