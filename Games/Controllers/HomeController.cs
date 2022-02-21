using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Games.Models;
using Microsoft.AspNet.Identity;

namespace Games.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = db.Categories.ToList();

            return View(list);
        }

        public ActionResult Details(int GameId)
        {
            var game = db.Games.Find(GameId);

            if (game == null)
            {
                return HttpNotFound();
            }
            Session["GameId"] = GameId;
            return View(game);
        }

        [Authorize]
        public ActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buy(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var GameId = (int)Session["GameId"];

            var game = new BuyTheGame();

            var check = db.BuyTheGames.Where(m => m.GameId == GameId && m.UserId == UserId).ToList();

            if (check.Count < 1)
            {
                game.UserId = UserId;
                game.GameId = GameId;
                game.Messsage = Message;
                game.Date = DateTime.Now;

                db.BuyTheGames.Add(game);
                db.SaveChanges();

                ViewBag.Result = "The Message Is Sent Successfully";
            }
            else
            {
                ViewBag.Result = "Sorry You Aready Sent The Message Before";
            }


            return View();
        }

        [Authorize]
        public ActionResult GetGameByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Games = db.BuyTheGames.Where(m => m.UserId == UserId);
            return View(Games.ToList());
        }

        [Authorize]
        public ActionResult DetailsOfGame(int id)
        {
            var game = db.BuyTheGames.Find(id);

            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        //[Authorize]
        public ActionResult GetGamePublisher()
        {
            var UserId = User.Identity.GetUserId();

            var Games = from app in db.BuyTheGames
                        join game in db.Games
                        on 
                        app.GameId equals game.Id
                        where game.User.Id == UserId
                        select app;

            var grouped = from j in Games
                          group j by j.game.GameTitle
                          into gr
                          select new GamesViewModel
                          {
                              GameTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }
            

         public ActionResult Edit(int id)
        {
            var game = db.BuyTheGames.Find(id);

            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(BuyTheGame game)
        {
           if(ModelState.IsValid)
            {

                game.Date = DateTime.Now;
                db.Entry(game).State = EntityState.Modified;


                db.SaveChanges();
                return RedirectToAction("GetGameByUser");
            }

            return View(game);
        }


        public ActionResult Delete(int id)
        {
            var game = db.BuyTheGames.Find(id);

            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(BuyTheGame game)
        {
            try
            {
                // TODO: Add delete logic here
                var myGame = db.Roles.Find(game.Id);
                db.Roles.Remove(myGame);
                db.SaveChanges();
                return RedirectToAction("GetGameByUser");
            }
            catch
            {
                return View(game);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Setting()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {

            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("ahmedabdelatyeelu@gmail.com", "Password");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("ahmedabdelatyeelu@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "Name Of Sender" + contact.Name + "<br />" +
                           "Email" + contact.Email + "<br />" +
                           "Subject Of Message" + contact.Subject + "<br />" +
                           "The Message" + contact.Message + "<br />";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }


        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Games.Where(a => a.GameTitle.Contains(searchName)
            || a.GameContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();

            return View(result);
        }
    }
}