using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using BitBookProj.DAL;
using BitBookProj.Models;

namespace BitBookProj.BLL
{
    public class UserManager
    {
        UserGateway _userGateway=new UserGateway();

        public bool Add(User user)
        {
            return _userGateway.Add(user);
        }

        public bool Update(User user)
        {
            return _userGateway.Update(user);
        }

        public bool Delete(User user)
        {
            return _userGateway.Delete(user);
        }

        public User GetById(int id)
        {
            return _userGateway.GetById(id);
        }

        public User GetByEmail(string email)
        {
            return _userGateway.GetByEmail(email);
        }

        public List<User> GetBySearchString(string name)
        {
            return _userGateway.GetBySearchString(name);
        }

        public List<User> GetAll()
        {
            return _userGateway.GetAll();
        }
    }
}