using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia
{
    internal class User
    {
        private string _handle;
        private string _description;
        private List<User> _friendslist;

        public User(string handle, string description)
        {
            _handle = handle;
            _description = description;
            _friendslist = new List<User>();
        }

        public List<User> FriendsList()
        {
            return _friendslist;
        }

        public string Handle()
        {
            return _handle;
        }

        public string Description()
        {
            return _description;
        }

        public void UpdateDescription(string desc)
        {
            _description = desc;
        }
    }
}
