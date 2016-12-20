using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookProj.Models;

namespace BitBookProj.DAL
{
    public class StatusGateway
    {
        BitBookDbContext db=new BitBookDbContext();

        public bool Add(Status status)
        {
            db.Status.Add(status);
            return db.SaveChanges() > 0;
        }

        public bool Delete(Status status)
        {
            db.Status.Remove(status);
            return db.SaveChanges() > 0;
        }

        public List<Status> GetStatusById(int id)
        {
            var statusList = db.Status.Where(c => c.UserId == id).OrderByDescending(c=>c.Date).ToList();
            return statusList;
        }

    }
}