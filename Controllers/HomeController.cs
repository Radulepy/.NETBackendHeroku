using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaduMVC.Models;

namespace RaduMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly InternshipClass _internshipClass;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _internshipClass = new InternshipClass();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_internshipClass);
        }
    }
}
