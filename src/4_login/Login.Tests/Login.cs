﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login.Tests
{
    public class App
    {
        public App(Func<DateTime> timeProvider)
        {
            this.timeProvider = timeProvider;
        }

        List<IEvent> history = new List<IEvent>();

        readonly Func<DateTime> timeProvider;

        public void Given(params IEvent[] events) => history.AddRange(events);

        public void Handle(LoginCommand command)
        {
            if (history.TooManyAttempts(timeProvider))
                throw new AuthenticationException("Too many attempts");
        }
    }

    public static class LoginRules
    {
        public static bool TooManyAttempts(this IEnumerable<IEvent> events, Func<DateTime> timeProvider)
            => events
            .OfType<AuthenticationAttemptFailedEvent>()
            .Where(x => x.Time >= timeProvider().AddMinutes(-15))
            .Count() <= 3;
    }
}
