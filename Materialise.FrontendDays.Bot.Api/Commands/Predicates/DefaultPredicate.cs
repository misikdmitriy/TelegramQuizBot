﻿using System.Threading.Tasks;
using Materialise.FrontendDays.Bot.Api.Commands.Predicates.Contracts;
using Telegram.Bot.Types;

namespace Materialise.FrontendDays.Bot.Api.Commands.Predicates
{
    public class DefaultPredicate : ICommandPredicate
    {
        public bool IsDefault => true;

        public Task<bool> IsThisCommand(Update update)
        {
            return Task.FromResult(false);
        }
    }
}