using Final_Assignment.Models;
using Inventory_with_Repository_Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Assignment.Repositories
{
    public class UserRepository : Repository<User>
    {

        public bool Validate(User u)
        {
            User user = this.GetAll().Where<User>(x => x.Username == u.Username && x.Password == u.Password).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int getIdbyUserName(User u)
        {
            User user = this.GetAll().Where<User>(x => x.Username == u.Username).FirstOrDefault();
            return user.UserID;
        }
    }
}