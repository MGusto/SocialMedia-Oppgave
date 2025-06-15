using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia
{
    internal class App
    {
        // to-do :
        // -legge til ekstra brukere i userbase??
        public static void Run()
        {
            var userbase = new List<User>
            {
                new User("blorbo16", "hello i am blorbo hehe"),
                new User("TheEpicDork1988", "Epic Millennial Dork X D"),
                new User("Joe Pluck", "official page of country artist joe pluck. god bless USA."),
                new User("hack_r_girl", "Stop sending me weird messages and random friend requests, seriously"),
                new User("OLO", "i am a pooboy lmao"),
                new User("IDM-121183945", "jp japan 北海道, idm fan"),
                new User("Official DcMonalds",
                    "Official burger DcMonalds friendFace™ account please go to our website at www.dcmonalds.gov.uk")
            };
            Console.WriteLine("Welcome to friendFace™\nPlease create an account.\n");
            Thread.Sleep(500);
            Console.WriteLine("Enter Username:\n");
            var username = Console.ReadLine();
            Console.WriteLine("Enter Password:\n");
            var password = Console.ReadLine();
            Console.Clear();
            Thread.Sleep(500);
            Console.WriteLine($"User account created.\nWelcome, {username}!\n");
            Thread.Sleep(500);
            Console.WriteLine("It's time to create a profile.\nEnter your public handle / nickname below:\n");
            var handle = Console.ReadLine();
            Console.WriteLine(
                "Handle is available!\nNext, you should write a short description that users can read when checking your profile:\n");
            var description = Console.ReadLine();
            Console.WriteLine("Wonderful.\nWe're creating your profile now.\n");
            Thread.Sleep(1000);
            Account account = new Account(username, password);
            User you = new User(handle, description);
            userbase.Add(you);
            Console.Clear();
            Console.WriteLine("Public profile created!\nPress any key to continue to the main feed...");
            Console.ReadKey();
            bool accountmade = true;
            while (accountmade)
            {
                Console.Clear();
                Console.WriteLine($"You are logged in on account '{account.Username()}'");
                Console.WriteLine($"Public handle: {you.Handle()}");
                Console.WriteLine(
                    "\n\nAvailable commands:\n'Online' - Check online users\n'Me' - Check your own profile\n'Friends' - Check your friends list\n'Exit' - Exit the program");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "online":
                        Console.Clear();
                        OnlineUsers(userbase, you);
                        break;
                    case "cheatmode":
                        foreach (var user in userbase)
                        {
                            if (user != you)
                                you.FriendsList().Add(user);
                        }
                        break;
                    case "me":
                        Console.Clear();
                        CheckProfile(you, you);
                        break;
                    case "friends":
                        CheckFriends(you, you);
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void OnlineUsers(List<User> userbase, User currentUser)
        {
            //viser "online" brukere, kan legge til venner herfra
            while (true)
            {
                var usercount = 1;
                Console.Clear();
                Console.WriteLine(
                    "- Online users - \n\nAvailable commands:\nType in user handle (case sensitive) or corresponding number to check their profile.\n'Exit' - Return to previous page\n\n");
                for (int i = 0; i < userbase.Count; i++)
                {
                    var friendstring = "";
                    var userstring = "";

                    if (currentUser.FriendsList().Contains(userbase[i]))
                    {
                        friendstring = "[This user is your friend]";
                    }
                    if (userbase[i] == currentUser)
                    {
                        userstring = "[You]";
                    }

                    usercount++;
                    Console.WriteLine($"{i}). {userbase[i].Handle()} {friendstring} {userstring}\n");
                }
                Console.WriteLine($"{usercount} total user(s) online.");
                var input = Console.ReadLine();
                if (input == null) continue;
                input = input.Trim();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (int.TryParse(input, out int index))
                {
                    if (index >= 0 && index < userbase.Count)
                    {
                        CheckProfile(currentUser, userbase[index]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid number. Press any key to try again.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    var found = userbase.FirstOrDefault(u => u.Handle().Equals(input, StringComparison.OrdinalIgnoreCase));
                    if (found != null)
                    {
                        CheckProfile(currentUser, found);
                    }
                    else
                    {
                        Console.WriteLine("No user found with that handle. Press any key to try again.");
                        Console.ReadKey();
                    }
                }
            }
        }

        public static void CheckProfile(User currentUser, User profileUser)
        {
            //metode for å sjekke og displaye profilinformasjon (bruker også for deg)
            Console.Clear();
            Console.WriteLine("- Profile View -\n");
            Console.WriteLine($"Handle: {profileUser.Handle()}\n");
            Console.WriteLine($"Description:\n{profileUser.Description()}\n");
            Console.WriteLine($"{profileUser.Handle()} has " + FriendCounter(profileUser) + " friend(s).\n");

            if (currentUser == profileUser)
            {
                // se på egen profil
                Console.WriteLine(
                    "\nAvailable commands:\n'Edit' - Write a new description.\n'Friends' - Check your friends list\n'Exit' - Return to previous page");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "edit":
                        Console.Clear();
                        Console.WriteLine($"Your current description:\n{profileUser.Description()}\n");
                        Console.WriteLine("Write a new description:\n");
                        var newDescription = Console.ReadLine();
                        profileUser.UpdateDescription(newDescription);
                        Console.WriteLine(
                            "You have successfully replaced your description.\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        CheckProfile(currentUser, profileUser);
                        break;
                    case "friends":
                        CheckFriends(currentUser, profileUser);
                        break;
                    case "exit":
                        break;
                }
            }
            //se på annen profil (ikke venn)
            else if (!currentUser.FriendsList().Contains(profileUser))
            {
                Console.WriteLine(
                    "\nAvailable commands:\n'Add' - Add user as friend.\n'Friends' - Check user's friends list\n'Exit' - Return to previous page");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "add":
                        Console.Clear();
                        AddFriend(currentUser, profileUser);
                        break;
                    case "friends":
                        CheckFriends(currentUser, profileUser);
                        break;
                    case "exit":
                        break;
                }
            } else { 
                // se på annen profil (venn)
                Console.WriteLine(
                    "\nAvailable commands:\n'Remove' - Remove user as friend.\n'Friends' - Check user's friends list\n'Exit' - Return to previous page");
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "remove":
                        Console.Clear();
                        RemoveFriend(currentUser, profileUser);
                        break;
                    case "friends":
                        CheckFriends(currentUser, profileUser);
                        break;
                    case "exit":
                        break;
                }
            }
        }

        public static void CheckFriends(User currentUser, User profileUser)
        {
            //metode som sjekker og displayer venneliste til profil (brukes også for din egen venneliste)
            while (true)
            {
                Console.Clear();
                var profileFriends = profileUser.FriendsList();
                var friendcount = 1;
                Console.Clear();
                Console.WriteLine(
                    $"- {profileUser.Handle()}'s Friends List - \n\nAvailable commands:\nType in user handle (case sensitive) or corresponding number to check their profile.\n'Exit' - Return to previous page\n\n");
                for (int i = 0; i < profileFriends.Count; i++)
                {

                    friendcount++;
                    Console.WriteLine($"{i}). {profileFriends[i].Handle()} - Online\n");
                }
                Console.WriteLine($"{profileUser.Handle()} has {friendcount} friend(s).\n");
                var input = Console.ReadLine();
                if (input == null) continue;
                input = input.Trim();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (int.TryParse(input, out int index))
                {
                    if (index >= 0 && index < profileFriends.Count)
                    {
                        CheckProfile(currentUser, profileFriends[index]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid number. Press any key to try again.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    var found = profileFriends.FirstOrDefault(u =>
                        u.Handle().Equals(input, StringComparison.OrdinalIgnoreCase));
                    if (found != null)
                    {
                        CheckProfile(currentUser, found);
                    }
                    else
                    {
                        Console.WriteLine("No user found with that handle. Press any key to try again.");
                        Console.ReadKey();
                    }
                }
            }
        }

        //metode for å legge til venner
        public static void AddFriend(User currentUser, User profileUser)
        {
            Console.Clear();
            Console.WriteLine($"Do you wish to send {profileUser.Handle()} a friend request?\n(Y / N)\n");
            var input = Console.ReadLine().ToLower();
            if (input == "y")
            {
                Console.WriteLine(
                    $"{profileUser.Handle()} has accepted your friend request!\nPress any key to continue...");
                currentUser.FriendsList().Add(profileUser);
                profileUser.FriendsList().Add(currentUser);
                Console.ReadKey();
                CheckProfile(currentUser, profileUser);
            }
            else
            {
                CheckProfile(currentUser, profileUser);
            }
        }

        //metode for å fjerne venner
        public static void RemoveFriend(User currentUser, User profileUser)
        {
            Console.Clear();
            Console.WriteLine($"Do you wish to remove {profileUser.Handle()} from your friends list?\n(Y / N)\n");
            var input = Console.ReadLine().ToLower();
            if (input == "y")
            {
                Console.WriteLine(
                    $"{profileUser.Handle()} has been removed from your friends list.\nPress any key to continue...");
                currentUser.FriendsList().Remove(profileUser);
                profileUser.FriendsList().Remove(currentUser);
                Console.ReadKey();
                CheckProfile(currentUser, profileUser);
            }
            else
            {
                CheckProfile(currentUser, profileUser);
            }
        }

        //metode for å telle antall venner utenfor CheckFriends() metoden
        static int FriendCounter(User user)
                {
                    int friendCount = 0;
                    foreach (var friend in user.FriendsList())
                    {
                        friendCount++;
                    }
                    return friendCount;
                }
    }
}




