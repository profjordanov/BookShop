using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.Core
{
    public struct Success
    {
        public Success(IEnumerable<string> messages)
            : this(messages.ToArray())
        {
        }

        public Success(params string[] messages)
        {
            Messages = messages;
            Date = DateTime.Now;
        }

        public IReadOnlyList<string> Messages { get; }

        public DateTime Date { get; }
    }
}