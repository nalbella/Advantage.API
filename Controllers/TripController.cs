using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class TripController : Controller
    {

        private readonly ApiContext _ctx;

        public TripController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // Get api/trip/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Trips.Include(o => o.Account)
                .OrderByDescending(c => c.RTA);

            var page = new PaginatedResponse<Trip>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByStatus")]
        public IActionResult ByStatus()
        {
            var trips = _ctx.Trips.Include(o => o.Account)
                .ToList();

            trips = trips.Where(c => c.Account != null).ToList();

            var groupedResult = trips.GroupBy(o => o.Account.AccountStatus)
                .ToList()
                .Select(grp => new
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.QuotedPrice)
                }).OrderByDescending(res => res.Total)
                .ToList();

            return Ok(groupedResult);
                
        }

        [HttpGet("ByAccount/{n}")]
        public IActionResult ByAccount(int n)
        {
            var trips = _ctx.Trips.Include(o => o.Account)
                .ToList();

            trips = trips.Where(c => c.Account != null).ToList();

            var groupedResult = trips.GroupBy(o => o.Account.Id)
                .ToList()
                .Select(grp => new
                {
                    Name = _ctx.Accounts.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.QuotedPrice)
                }).OrderByDescending(res => res.Total)
                .Take(n)
                .ToList();

            return Ok(groupedResult);

        }

        [HttpGet("GetTrip/{id}", Name = "GetTrip")]
        public IActionResult GetTrip(int id)
        {
            var trip = _ctx.Trips.Include(o => o.Account)
                .First(o => o.Id == id);

            return Ok(trip);
        }
    }
}
