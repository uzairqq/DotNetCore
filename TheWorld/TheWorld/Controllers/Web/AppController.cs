using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
  public class AppController : Controller
  {
    private IMailService _mailService;
    private IConfigurationRoot _config;
      private readonly IWorldRepository _repository;
      private readonly ILogger<AppController> _logger;


      public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository,ILogger<AppController> logger)
    {
      _mailService = mailService;
      _config = config;
        _repository = repository;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
        [Authorize]//for using of identiy for this action
      public IActionResult Trips()
      {
          try
          {
              var trips = _repository.GetAllTrips();
              return View(trips);
          }
          catch (Exception e)
          {
              Console.WriteLine(e);
              _logger.LogError($"Failed To Get Trips in Index Page:{e.Message}");
              return Redirect("/Error");

          }
      }

        public IActionResult Contact()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Contact(ContactViewModel model)
    {
      if (model.Email.Contains("aol.com"))
      {
        ModelState.AddModelError("", "We don't support AOL addresses");
      }

      if (ModelState.IsValid)
      {
        _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From TheWorld", model.Message);

        ModelState.Clear();

        ViewBag.UserMessage = "Message Sent";
      }

      return View();
    }

    public IActionResult About()
    {
      return View();
    }

  }
}
