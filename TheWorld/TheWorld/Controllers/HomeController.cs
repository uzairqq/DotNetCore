using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailServices _mailServices;
        private readonly IConfigurationRoot _configuration;

        public HomeController(IMailServices mailServices, IConfigurationRoot configuration)
        {
            _mailServices = mailServices;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
           
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            { 
            _mailServices.SendMail(_configuration["MailSettings:ToAddress"],"Laraib.aiit@hotmail.com","TheWorld","HelloUzairss");
            }
            return View();
        }

        
    }
}
