using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BitBookProj.Models;

namespace BitBookProj.DAL
{
    public class UserGateway
    {
        private BitBookDbContext db= new BitBookDbContext();

        public bool Add(User user)
        {
           db.Users.Add(user);
           return  db.SaveChanges()>0;

        }

        public bool Update(User user)
        {
            db.Users.Attach(user);
            db.Entry(user).State= EntityState.Modified;
            return db.SaveChanges()>0;
        }

        public bool Delete(User user)
        {
            db.Users.Remove(user);
            return db.SaveChanges()>0;
        }

        public User GetById(int id)
        {
            User user = db.Users.FirstOrDefault(c => c.Id==id);

            return user;
        }

        public User GetByEmail(string email)
        {
            User user = db.Users.FirstOrDefault(c => c.Email == email);

            return user;
        }

        public List<User> GetBySearchString(string name)
        {
            var users = db.Users.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name)).ToList();

            return users;
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }
    }
}