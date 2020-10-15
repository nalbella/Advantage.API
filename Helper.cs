using System;
using System.Collections.Generic;

namespace Advantage.API
{
    public class Helper
    {
        private static Random _rand = new Random();

        private static string GetRandom(IList<string> items)
        {
            return items[_rand.Next(items.Count)];
        }

        internal static string MakeAccountName()
        {
            var name = GetRandom(bizName);
            var surname = GetRandom(bizSurname);
            return name + ' ' + surname;
        }

        internal static string MakeAccountEmail(string accountName)
        {
            return $"contact@{MakeDomainName().ToLower()}";
        }

        internal static string MakeDomainName()
        {
            return GetRandom(bizDomain);
        }

        internal static string GetRandomState()
        {
            return GetRandom(states);
        }

        internal static decimal GetRandomQuotedPrice()
        {
            return _rand.Next(10, 80);
        }

        internal static DateTime GetRandomCreatedDate()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan= new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }

        internal static DateTime? GetRandomRTA(DateTime createdDate)
        {
            return createdDate.AddHours(_rand.Next(0, 300));
        } 

        internal static Boolean GetRandomActive()
        {
            var intActive = _rand.Next(0, 1);

            return intActive == 1;
        }

        private static readonly List<string> states = new List<string> ()
        {
            "Open",
            "Closed",
            "New Account",
            "Credit Not Approved",
            "On Stop",
            "Lost",
            "Stolen",
            "Open (Corporate only)",
            "Open (Personal only)",
            "On Stop (exceeded logon attempts)",
            "Payment details not registered",
            "Payment details expired",
            "Payment outstanding"

        };

        private static readonly List<string> bizName = new List<string>()
        {
            "Thomas",
            "John",
            "Anthony",
            "Peter",
            "Fran",
            "Adam",
            "Allan",
            "Donald",
            "Gerard",
            "James"
        };

        private static readonly List<string> bizSurname = new List<string>()
        {
            "Bercow",
            "Kennedy",
            "Black",
            "Brown",
            "Sterling",
            "Cameron",
            "Bale",
            "Giggs",
            "Williams",
            "Johnson"
        };

        private static readonly List<string> bizDomain = new List<string>()
        {
            "microsoft.com",
            "jpm.co.uk",
            "deutchbank.co.uk",
            "cityfleet.co.uk",
            "comcab.co.uk",
            "gs.com",
            "hsbc.co.uk",
            "linklaters.com",
            "mercers.co.uk",
            "aol.com"
        };


    }
}