﻿using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Chat.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Models;
using System.Threading;

namespace Chat.Business.Providers
{
    public class UserProvider : IUserService
    {
        private UserList Users { get; set; }
        private readonly object _lockList = new object();

        public UserProvider()
        {
            Users = new UserList();
            Users.OnAdd += Users_OnAdd;
            Users.OnRemove += Users_OnRemove;

            var disconnectedUserTask = new Task(CheckAlive);
            disconnectedUserTask.Start();
        }

        void Users_OnRemove(object sender, EventArgs e)
        {
            
        }

        void Users_OnAdd(object sender, EventArgs e)
        {
            
        }

        public bool Login(string username)
        {
            lock (_lockList)
            {
                if (IsUserRegistered(username))
                    return false;

                Users.Add(new User
                    {
                        Username = username.ToLower(),
                        ConnectionDate = DateTime.Now,
                        LastCheck = DateTime.Now
                    });
            }
            return true;
        }

        public void Logout(string username)
        {
            lock (_lockList)
            {
                if (IsUserRegistered(username))
                    Users.Remove(Users.Single(p => p.Username == username));
            }
        }

        public bool IsUserRegistered(string username)
        {
            return Users.SingleOrDefault(p => p.Username == username.ToLower()) != null;
        }

        public IList<string> GetUsers()
        {
            return Users.OrderBy(p => p.Username).Select(p => p.Username).ToList();
        }

        private void CheckAlive()
        {
            while (true)
            {
                lock (_lockList)
                {
                    var disconnectedUsers =
                        Users.Where(p => p.LastCheck.AddSeconds(90) < DateTime.Now).Select(p => p.Username).ToList();
                    foreach (var disconnectedUser in disconnectedUsers)
                    {
                        Users.Remove(Users.Single(p => p.Username == disconnectedUser));
                    }
                }

                Thread.Sleep(10000);
            }
        }
    }
}