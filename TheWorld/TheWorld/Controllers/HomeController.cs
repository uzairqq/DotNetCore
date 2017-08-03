using System;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailServices _mailServices;

        public HomeController(IMailServices mailServices)
        {
            _mailServices = mailServices;
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
            _mailServices.SendMail("uzair.qq@outlook.com","Laraib.aiit@hotmail.com","TheWorld","HelloUzairss");
            return View();
        }

        
    }
}
