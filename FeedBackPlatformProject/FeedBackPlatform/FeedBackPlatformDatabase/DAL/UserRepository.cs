using FeedBackPlatformDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedBackPlatformDatabase.Context;

namespace FeedBackPlatformDatabase.DAL
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(FeedBackPlatformContext context) : base(context)
        {
        }



        public bool IsEmailUsed(string email)
        {
            return dbSet.Any(e => e.Email == email);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var user = dbSet.FirstOrDefault(e => e.Email == email);
            if (user != null && user.Password.Equals(password, StringComparison.InvariantCulture))
            {
                return user;
            }

            return null;
        }
    }
}
