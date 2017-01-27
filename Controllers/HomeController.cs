using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iGotTheRuns.Data;
using iGotTheRuns.Models;
using iGotTheRuns.Services;

namespace iGotTheRuns.Controllers {
    public class HomeController : Controller {
        private IMailService _mailService;
        private IMessageBoardRepository _repo;

        //inject IMailService
        public HomeController(IMailService mailService, IMessageBoardRepository repo) {
            _mailService = mailService;
            _repo = repo;
        }

        public ActionResult Index() {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var topics = _repo.GetTopics()
                                .OrderByDescending(t => t.Created)
                                .Take(25)
                                .ToList();

            return View(topics);
        }

        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model) {
            var msg = string.Format("Comment From: {1}{0}Email:{2}{0}Website: {3}{0}",
                Environment.NewLine,
                model.Name,
                model.Email,
                model.Website,
                model.Comment);

            if(_mailService.SendMail("noreply@yourdomain.com",
                "greg.harris@walmart.com",
                "Hi Big Boy",
                msg)) {
                    ViewBag.MailSent = true;
                    ViewBag.Message = msg;
            }
            
            return View();
        }

        [Authorize]
        public ActionResult MyMessages() {
            return View();
        }

        public ActionResult Runs() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Index2()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var topics = _repo.GetTopics()
                                .OrderByDescending(t => t.Created)
                                .Take(25)
                                .ToList();

            return View(topics);
        }

    }
}
