using System;
using System.Collections.Generic;
using Chat.Models;

namespace Chat.Business.Providers
{
    public class UserList : List<User>
    {
        public event EventHandler OnAdd;
        public event EventHandler OnRemove;

        public new void Add(User item)
        {
            OnAdd(this, null);
            base.Add(item);
        }

        public new void Remove(User item)
        {
            OnRemove(this, null);
            base.Remove(item);
        }
    }
}