using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {

        private readonly DataContext Context;

        public ValuesController (DataContext context) {
            this.Context = context;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues () {
            var val = await this.Context.Values.ToListAsync ();
            return Ok (val);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetValues (int id) {
            var val = await this.Context.Values.FirstOrDefaultAsync (x => x.Id == id);
            return Ok (val);
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}