using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaduMVC.Services;
using RaduMVC.Data;
using RaduMVC.Models;

namespace RaduMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InternshipService intershipService;
        private readonly MessageService messageService;

        public HomeController(ILogger<HomeController> logger, InternshipService intershipService, MessageService messageService)
        {
            _logger = logger;
            this.intershipService = intershipService;
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            return View(intershipService.GetClass());
        }

        public IActionResult Privacy()
        {
            return View(intershipService.GetClass());
        }

        public IActionResult Chat()
        {

            return View(messageService.GetAllMessages());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            intershipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            return intershipService.AddMember(intern);
        }

        [HttpPut]
        public void UpdateMember(int id, string memberName)
        {
            Intern intern = new Intern();
            intern.Id = id;
            intern.Name = memberName;
            intershipService.UpdateMember(intern.Id, intern.Name);
        }


    }
}
