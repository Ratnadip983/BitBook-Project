using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookProj.BLL;
using BitBookProj.Models;

namespace BitBookProj.Controllers
{
    public class StatusController : Controller
    {
       

         [HttpPost]
        public ActionResult PostStatus([Bind(Include = "UserId, Text")]Status status)
        {
            
            status.Date=DateTime.Now;
            StatusManager statusManager=new StatusManager();
            if (ModelState.IsValid)
            {
                statusManager.Add(status);
            }
            return RedirectToAction("NewsFeed", "Users");
        }

        public ActionResult LikeStatus([Bind(Include = "UserId,StatusId")] Like like)
        {
            LikeManager likeManager=new LikeManager();
            if (ModelState.IsValid)
            {
                likeManager.Add(like);
            }
            return RedirectToAction("NewsFeed", "Users");
        }
    }
}