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
        // -smelle ut alle hovedfunksjoner (online brukere, friends list view fra main menu + CheckProfile(), osv)
        // -trimme ubrukelig shit fra bl.a. User
        // -legge til ekstra brukere i userbase??
        public static void Run()
        {
            var userbase = new List<User>
            {
                new User("blorbo16", "hello i am blorbo hehe", false, false),
                new User("TheEpicDork1988", "Epic Millennial Dork X D", false, false),
                new User("Joe Pluck", "official page of country artist joe pluck. god bless USA.", false, false),
                new User("hack_r_girl", "Stop sending me weird messages and random friend requests, seriously", false,
                    false),
                new User("OLO", "i am a pooboy lmao", false, false),
                new User("IDM-121183945", "jp japan 北海道, idm fan", false, false),
                new User("Official DcMonalds",
                    "Official burger DcMonalds friendFace™ account please go to our website at www.dcmonalds.gov.uk",
                    false, false)
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
            User you = new User(handle, description, false, true);
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
                var commandinput = Console.ReadLine().ToLower();
                switch (commandinput)
                {
                    case "online":
                        Console.Clear();
                        OnlineUsers(userbase, you);
                        break;
                    case "cheatmode":
                        you.FriendsList().Add(userbase[1-7]);
                        break;
                    case "me":
                        Console.Clear();
                        CheckProfile(you, you);
                        break;
                    case "friends":
                        break;
                    case "exit":
                        break;
                }
            }
        }

        public static void CheckProfile(User currentUser, User profileUser)
        {
            Console.Clear();
            Console.WriteLine("- Profile View -\n");
            Console.WriteLine($"Handle: {profileUser.Handle()}\n");
            Console.WriteLine($"Description:\n{profileUser.Description()}\n");
            Console.WriteLine($"{profileUser.Handle()} has " + FriendCounter(profileUser) + " friend(s).\n");

            if (currentUser == profileUser)
            {
                // se på egen profil
                Console.WriteLine(
                    "\nAvailable commands:\n'Edit' - Write a new description.\n'Friends' - Check your friends list\n'Exit' - Return to menu");
                var selfProfileInput = Console.ReadLine().ToLower();
                switch (selfProfileInput)
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
                        Console.WriteLine("WIP");
                        Console.ReadKey();
                        break;
                    case "exit":
                        Console.WriteLine("WIP");
                        Console.ReadKey();
                        break;
                }
            }
            //se på annen profil (ikke venn)
            else if (!currentUser.FriendsList().Contains(profileUser))
            {
                Console.WriteLine(
                    "\nAvailable commands:\n'Add' - Add user as friend.\n'Friends' - Check user's friends list\n'Exit' - Return to menu");
                var strangerProfileInput = Console.ReadLine().ToLower();
                switch (strangerProfileInput)
                {
                    case "add":
                        Console.Clear();
                        Console.WriteLine($"Do you wish to send {profileUser.Handle()} a friend request?\n(Y / N)\n");
                        var requestFriendship = Console.ReadLine().ToLower();
                        if (requestFriendship == "y")
                        {
                            Console.WriteLine(
                                $"{profileUser.Handle()} has accepted your friend request!\nPress any key to continue...");
                            currentUser.FriendsList().Add(profileUser);
                            //profileUser.isfriend = true; HUSK trenger ikke denne boolen lenger, bruk friendslist
                        }
                        else
                        {
                            CheckProfile(currentUser, profileUser);
                        }
                        Console.ReadKey();
                        CheckProfile(currentUser, profileUser);
                        break;
                    case "friends":
                        Console.WriteLine("WIP");
                        Console.ReadKey();
                        break;
                    case "exit":
                        Console.WriteLine("WIP");
                        Console.ReadKey();
                        break;
                }
            } else { 
                // se på annen profil (venn)
                Console.WriteLine(
                    "\nAvailable commands:\n'Remove' - Remove user as friend.\n'Friends' - Check user's friends list\n'Exit' - Return to menu");
                var strangerProfileInput = Console.ReadLine().ToLower();
                switch (strangerProfileInput)
                {
                    case "add":
                        Console.Clear();
                        Console.WriteLine($"Do you wish to remove {profileUser.Handle()} from your friends list?\n(Y / N)\n");
                        var requestFriendship = Console.ReadLine().ToLower();
                        if (requestFriendship == "y")
                        {
                            Console.WriteLine(
                                $"{profileUser.Handle()} has been removed from your friends list.\nPress any key to continue...");
                            currentUser.FriendsList().Remove(profileUser);
                            //profileUser.isfriend = false; HUSK trenger ikke denne boolen lenger, bruk friendslist
                        }
                        else
                        {
                            CheckProfile(currentUser, profileUser);
                        }
                        Console.ReadKey();
                        CheckProfile(currentUser, profileUser);
                        break;
                    case "friends":
                        Console.WriteLine("WIP");
                        Console.ReadKey();
                        break;
                    case "exit":
                        Console.WriteLine("WIP");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static int FriendCounter(User user)
                {
                    int friendCount = 0;
                    foreach (var friend in user.FriendsList())
                    {
                        friendCount++;
                    }
                    return friendCount;
                }

        public static void OnlineUsers(List<User> userbase, User currentUser)
        {
            // trenger måte å sjekke input + kjøre en CheckProfile() med korresponderende bruker
            var usercount = 0;
            Console.Clear();
            Console.WriteLine(
                "- Online users - \n\nAvailable commands:\nType in user handle or corresponding number to check their profile.\n'Exit' - Return to menu.\n\n");
            for (int i = 0; userbase.Count > i; i++)
            {
                var friendstring = "";
                var userstring = "";

                //is user friend
                if (currentUser.FriendsList().Contains(userbase[i]))
                {
                    friendstring = "[This user is your friend]";
                }

                //is user you
                if (userbase[i] == currentUser)
                {
                    userstring = "[You]";
                }


                usercount++;
                Console.WriteLine($"{i}). {userbase[i].Handle()} {friendstring} {userstring}\n");
            }
            Console.WriteLine($"{usercount} total user(s) online.");
            Console.ReadLine();
        }
    }
    }

