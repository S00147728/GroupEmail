using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GroupEmailExample.Models;
using System.Net;
using System.Net.Mail;
//using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using SendGrid;

namespace GroupEmailExample.Controllers
{
    public class HomeController : Controller
    {
        private IOptions<ApplicationSettings> _settings;
        public HomeController(IOptions<ApplicationSettings> settings)
        {
            _settings = settings;
        }
        [HttpGet("GetSetting",Name ="GetSetting")]
        public IActionResult GetSetting()
        {
            var testSetting = _settings.Value.TestSetting;
            return new ObjectResult(testSetting);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [HttpGet("Contact", Name = "Contact")]
        public IActionResult Contact()
        {
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo("darraghmcgowan@ymail.com");
            myMessage.From = new MailAddress("paull1068@gmail.com", "Paul Little");
            myMessage.Subject = "Welcomes to Intstrings";
            myMessage.Text = "Testing";

            var credentials = new NetworkCredential(_settings.Value.Username, _settings.Value.Password);
            var transportWeb = new Web(credentials);
            transportWeb.DeliverAsync(myMessage);
            
           // return new OkObjectResult(true);
            return RedirectToAction("Sent");
        }
        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}
  
       
        public ActionResult Sent()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
    }

