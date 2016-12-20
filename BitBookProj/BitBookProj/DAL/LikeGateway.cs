using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookProj.Models;

namespace BitBookProj.DAL
{
    public class LikeGateway
    {
        BitBookDbContext db=new BitBookDbContext();
        public bool Add(Like like)
        {
            db.Likes.Add(like);
            return db.SaveChanges() > 0;
        }

        public bool Delete(Like like)
        {
            db.Likes.Remove(like);
            return db.SaveChanges() > 0;
        }


        public int LikeCount(int statusId)
        {
            return db.Likes.Count(c => c.StatusId == statusId);
        }
    }
}