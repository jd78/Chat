using System.Runtime.CompilerServices;
using Chat.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Models;

namespace Chat.Business.Providers
{
    public class UserProvider : IUserService
    {
        private List<User> Users { get; set; }

        public UserProvider()
        {
            Users = new List<User>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)] 
        public bool Login(string username)
        {
            if (IsUserRegistered(username))
                return false;

            Users.Add(new User
                {
                    Username = username.ToLower(),
                    ConnectionDate = DateTime.Now,
                    LastCheck = DateTime.Now
                });

            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Logout(string username)
        {
            if (IsUserRegistered(username))
                Users.Remove(Users.Single(p => p.Username == username));

        }

        public bool IsUserRegistered(string username)
        {
            return Users.SingleOrDefault(p => p.Username == username.ToLower()) != null;
        }

        public IList<string> GetUsers()
        {
            return Users.OrderBy(p => p.Username).Select(p => p.Username).ToList();
        }
    }
}