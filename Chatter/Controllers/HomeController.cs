using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using SendGridMail;
using SendGridMail.Transport;

namespace Chatter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize(Users = "shigmuppets16")]
        public ActionResult SendEmail(string username = null, string password = null)
        {
            if (!Request.IsSecureConnection && !Request.IsLocal)
            {
                ViewBag.Message = "Unauthorized";
                return View("Index");
            }

            username = username ?? ConfigurationManager.AppSettings["SENDGRID_USER"];
            password = password ?? ConfigurationManager.AppSettings["SENDGRID_PASS"];

            // create email object
            var myMessage = SendGrid.GetInstance();

            // add basic properties
            myMessage.From = new MailAddress("Emdaq <emdaqlabs@gmail.com>");
            myMessage.AddTo(new List<string>
                {
                    @"Alden Quimby <aldenquimby@gmail.com>",
                });
            myMessage.Subject = "Testing the SendGrid Library";
            myMessage.Html = "<p>Hello World!</p><br/><a href='http://www.emdaq.com'>Awesome site</a>";
            myMessage.Text = "Hello World plain text!";

            if (System.IO.File.Exists(@"C:\Users\AldenQuimby\Downloads\Dan's_Blog_List.vcf"))
            {
                myMessage.AddAttachment(@"C:\Users\AldenQuimby\Downloads\Dan's_Blog_List.vcf");
            }

            // add filters
            myMessage.EnableFooter("PLAIN TEXT FOOTER", "<p><em>HTML FOOTER</em></p>");
            myMessage.EnableClickTracking(true);
            myMessage.EnableOpenTracking();

            // send it
            var creds = new NetworkCredential(username, password);
            var transport = SMTP.GetInstance(creds);
            transport.Deliver(myMessage);

            ViewBag.Message = "Sent!";
            return View("Index");
        }
    }
}
