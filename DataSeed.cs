using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.API.Model;

namespace Advantage.API
{
    public class DataSeed
    {
        private readonly ApiContext _ctx;

        public DataSeed(ApiContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedData(int nAccounts, int ntrips, int nDrivers)
        {
            if (!_ctx.Accounts.Any())
            {
                SeedAccounts(nAccounts);
                _ctx.SaveChanges();
            }

            if (!_ctx.Trips.Any())
            {
                SeedTrips(ntrips);
                _ctx.SaveChanges();
            }

            if (!_ctx.Drivers.Any())
            {
                SeedDrivers(nDrivers);
                _ctx.SaveChanges();
            }

            
        }

        private void SeedAccounts(int n)
        {
            List<Account> accounts = BuildAccountList(n);

            foreach(var account in accounts)
            {
                _ctx.Accounts.Add(account);
            } 
        }

        private void SeedTrips(int n)
        {
            List<Trip> trips = BuildTripList(n);

            foreach(var trip in trips)
            {
                _ctx.Trips.Add(trip);
            }
        }

        private void SeedDrivers(int n)
        {
            List<Driver> drivers = BuildDriverList(n);

            foreach(var driver in drivers)
            {
                _ctx.Drivers.Add(driver);
            }
        }

        private List<Account> BuildAccountList(int nAccounts)
        {
            var accounts = new List<Account>();

            for(var i = 1; i <= nAccounts; i++)
            {
                var name = Helper.MakeAccountName();

                accounts.Add(new Account {
                    Id = i,
                    Name = name,
                    Email = Helper.MakeAccountEmail(name),
                    AccountStatus = Helper.GetRandomState()
                });
            }

            return accounts;
        }

        private List<Trip> BuildTripList(int nTrips)
        {
            var trips = new List<Trip>();
            var rand = new Random();

            for(var i = 1; i <= nTrips; i++)
            {
                var randAccountId = rand.Next(_ctx.Accounts.Count());
                var createdDate = Helper.GetRandomCreatedDate();
                var rta = Helper.GetRandomRTA(createdDate);
                

                trips.Add(new Trip {
                    Id = i,
                    Account = _ctx.Accounts.Where(c => c.Id == randAccountId).FirstOrDefault(),
                    QuotedPrice = Helper.GetRandomQuotedPrice(),
                    RTA = rta,
                    CreatedDate = createdDate
                });
            }

            return trips;
        }

        private List<Driver> BuildDriverList(int nDrivers)
        {
            var drivers = new List<Driver>();

            for(var i = 1; i <= nDrivers; i++)
            {
                var name = Helper.MakeAccountName();

                drivers.Add(new Driver {
                    Id = i,
                    Name = name,
                    IsActive = Helper.GetRandomActive()
                });
            }

            return drivers;
        }





    }
}