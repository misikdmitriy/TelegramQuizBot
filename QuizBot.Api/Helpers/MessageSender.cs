﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizBot.Api.Helpers.Interfaces;
using QuizBot.Api.Repositories.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = QuizBot.Api.Models.User;

namespace QuizBot.Api.Helpers
{
    public class MessageSender : IMessageSender
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<MessageSender> _logger;
        private readonly IDbRepository<User> _userRepository;

        public MessageSender(ITelegramBotClient botClient, ILogger<MessageSender> logger,
            IDbRepository<User> userRepository)
        {
            _botClient = botClient;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task SendTo(int userId, string message)
        {
            var user = (await _userRepository.FindAsync(x => x.Id == userId))
                .First();

            _logger.LogDebug($"Send next message to user {userId}: '{message}'");

            await _botClient.SendTextMessageAsync(user.ChatId, message, false, false, 0, new ReplyKeyboardHide());
        }

        public async Task SendTo(int userId, string message, params string[] options)
        {
            var user = (await _userRepository.FindAsync(x => x.Id == userId))
                .First();

            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = options
                    .Select(o => new[] {new KeyboardButton(o)})
                    .ToArray()
            };

            _logger.LogDebug($"Send next message to user {userId}: '{message}'");
            _logger.LogDebug($"Next answers: {options.Aggregate(string.Empty, (s, s1) => s + ";" + s1)}");

            await _botClient.SendTextMessageAsync(user.ChatId, message, false, false, 0, keyboard);
        }
    }
}