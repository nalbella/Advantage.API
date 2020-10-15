using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ApiContext _ctx;

        public AccountController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Accounts.OrderBy(c => c.Id);

            return Ok(data);
        }

        // Get api/account/5
        [HttpGet("{id}", Name = "GetAccount")]
        public IActionResult Get(int id)
        {
            var account = _ctx.Accounts.Find(id);

            return Ok(account);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest();
            }

            _ctx.Accounts.Add(account);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetAccount", new { id = account.Id }, account);
        }
    }
}
