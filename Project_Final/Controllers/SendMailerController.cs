


using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Project_Final.Models;


namespace Project_Final.Controllers

{
    
        public class SendMailerController : Controller
        {
            // GET: /SendMailer/
            public ActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public ViewResult Index(MailModel _objModelMail)
            {
                if (ModelState.IsValid)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(_objModelMail.To);
                    mail.From = new MailAddress(_objModelMail.From);
                    mail.Subject = _objModelMail.Subject;
                    string Body = _objModelMail.Body;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("email", "password"); // Enter senders email and password //make sure to enable low security for the email
                    smtp.EnableSsl = true;

                    smtp.Send(mail);

                    return View("Index", _objModelMail);
                }
                else
                {
                    return View();
                }
            }
        }
    }