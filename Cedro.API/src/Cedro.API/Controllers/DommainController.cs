using Cedro.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Controllers
{
    public class DommainController : Controller
    {
        private ConnectContext _ctx;

        public DommainController(ConnectContext ctx)
        {
             _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public ActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
