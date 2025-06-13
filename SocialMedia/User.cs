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
        private bool _isfriend;
        private bool _isuser;
        public User(string handle, string description, bool isfriend, bool isuser)
        {
            _handle = handle;
            _description = description;
            _friendslist = new List<User>();
            _isfriend = isfriend;
            _isuser = isuser;
        }

        public bool isuser { get; }

        public bool isfriend { get; set; }

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
