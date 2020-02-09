using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bloodhound.Models;
using Bloodhound.Core.Model;
using Newtonsoft.Json;
using System.IO;

namespace Bloodhound.Controllers
{
    public class HomeController : Controller
    {
        private readonly BloodhoundContext _context;

        public HomeController(BloodhoundContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            JsonSerializer jsonSerializer = JsonSerializer.Create();
            StringWriter sw = new StringWriter();
            jsonSerializer.Serialize(sw, this._context.v_OffenderLastLocation.ToList());

            ViewData["LastLocations"] = sw.ToString();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
