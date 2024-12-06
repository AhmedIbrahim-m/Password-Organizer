using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace passwordOrganizer
{
    internal class Program
    {
        public static readonly Dictionary<string, string> _passwordEnters = new();
        
        static void Main(string[] args)
        {
            
            
            // Read passwords and admin credentials from the file at the start
            ReadPasswords();

            // Check if admin key exists, if not, prompt to set it
            if (_passwordEnters.Count == 0 )
            {
                SetAdminKey();
            }

            Console.WriteLine("Enter the Admin UserName");
            var EnterdAdminKey = Console.ReadLine();
            Console.WriteLine("Enter Password for the Admin UserName ");
            var EnterdPassKey = Console.ReadLine();

            // Authenticate admin user
            if (GetAdminAccess(EnterdAdminKey, EnterdPassKey))
            {
                while (true)
                {
                    Console.WriteLine("Please Select an option:");
                    Console.WriteLine("[1] List all passwords ");
                    Console.WriteLine("[2] Add/change passwords ");
                    Console.WriteLine("[3] Get passwords ");
                    Console.WriteLine("[4] Delete passwords ");
                    var SelectedOption = Console.ReadLine();

                    if (SelectedOption == "1")
                        ListAllPasswords();
                    else if (SelectedOption == "2")
                        AddOrChangePasswords();
                    else if (SelectedOption == "3")
                        GetPasswords();
                    else if (SelectedOption == "4")
                        DeletePasswords();
                    else
                        Console.WriteLine("INVALID OPTION ");
                    Console.WriteLine("--------------");
                }
            }
            else
            {
                Console.WriteLine("Invalid Admin Credentials!");
            }
        }

        static void SetAdminKey()
        {
            Console.WriteLine("No admin key set! Please set an Admin UserName and Password.");

            // Set the admin credentials
            Console.WriteLine("Enter Admin UserName");
            var AdminUserName = Console.ReadLine();
            Console.WriteLine("Enter Admin Password");
            var AdminPassword = Console.ReadLine();

            // Add the admin key to the dictionary
            _passwordEnters.Add(AdminUserName, AdminPassword); // Store "admin" as the username key
           
            // Optionally, save the admin credentials to a file or elsewhere for persistence
            SavePasswords();
            Console.WriteLine("Admin credentials set successfully!");
        }

        static bool GetAdminAccess(string UserName, string Password)
        {
            if (_passwordEnters.ContainsKey(UserName)    )
            {
                if (_passwordEnters[UserName] == Password)
                {
                    return true;
                }
            }

            return false;
        }

        static void DeletePasswords()
        {
            Console.WriteLine("Enter website/ App Name ");
            var NameToDelete = Console.ReadLine();
            if (_passwordEnters.ContainsKey(NameToDelete))
            {
                _passwordEnters.Remove(NameToDelete);
                SavePasswords();
                Console.WriteLine("Password deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid name or not found!");
            }
        }

        static void GetPasswords()
        {
            Console.WriteLine("Enter website/ App Name ");
            var name = Console.ReadLine();
            if (_passwordEnters.ContainsKey(name))
                Console.WriteLine($"The password for {name} is {_passwordEnters[name]}");
            else
                Console.WriteLine("Invalid name or not found!");
        }

        static void AddOrChangePasswords()
        {
            Console.WriteLine("Please enter website name ");
            var WebsiteName = Console.ReadLine();
            Console.WriteLine("Enter the password for the website ");
            var WebsitePassword = Console.ReadLine();

            if (_passwordEnters.ContainsKey(WebsiteName))
            {
                _passwordEnters[WebsiteName] = WebsitePassword;
            }
            else
            {
                _passwordEnters.Add(WebsiteName,Encryption.Encrypt( WebsitePassword));
            }

            SavePasswords();
            Console.WriteLine("Password added/changed successfully.");
        }

        static void SavePasswords()
        {
            var sb = new StringBuilder();
            foreach (var entry in _passwordEnters)
            {
                sb.AppendLine($"{entry.Key}={entry.Value}");
            }
            File.WriteAllText("C:/Users/ah7ch/Downloads/passwords.txt", sb.ToString());
        }

        static void ReadPasswords()
        {
            // Check if the file exists
            if (File.Exists("C:/Users/ah7ch/Downloads/passwords.txt"))
            {
                var PasswordsLine = File.ReadAllText("C:/Users/ah7ch/Downloads/passwords.txt");
                foreach (var Line in PasswordsLine.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(Line))
                    {
                        var equalidx = Line.IndexOf('=');
                        var web_App_Name = Line.Substring(0, equalidx);
                        var Password = Line.Substring(equalidx + 1);
                        _passwordEnters.Add(web_App_Name, Password);
                    }
                }
            }
        }

        static void ListAllPasswords()
        {
            foreach (var entry in _passwordEnters)
            {
                Console.WriteLine($"{entry.Key}={ Encryption.Decrypt( entry.Value)}");
            }
        }
    }
}
