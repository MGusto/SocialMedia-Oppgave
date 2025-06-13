namespace SocialMedia
{
    internal class Account
    {
        private string _username;
        private string _password;

        internal Account(string username, string password)
        {
            _username = username;
            _password = password;
        }
        public string Username()
        {
            return _username;
        }
    }
}
