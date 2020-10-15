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
    public class DriverController : Controller
    {

        private readonly ApiContext _ctx;

        public DriverController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Drivers.OrderBy(s => s.Id).ToList();
            return Ok(response);
        }

        [HttpGet("{id}", Name="GetDriver")]
        public IActionResult Index(int id)
        {
            var response = _ctx.Drivers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] DriverMessage msg)
        {
            var driver = _ctx.Drivers.Find(id);

            if (driver == null)
            {
                return NotFound();
            }

            // Refactor: move into a service
            if (msg.Payload == "activate")
            {
                driver.IsActive = true;
            }

            if (msg.Payload == "deactivate")
            {
                driver.IsActive = false;
            }

            _ctx.SaveChanges();

            return new NoContentResult();
        }
    }
}
