using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
 

namespace WebApplication3.Model
{
    public class UserCommand
    {

        public UserCommand(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("message", nameof(userId));
            }

            UserId = userId;
        }

        public string UserId { get; set; }
        public string UserProfile { get; set; }
        public void ExecuteCommand()
        {
            string str = CommandScript();

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C " + str,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                UserProfile = proc.StandardOutput.ReadToEnd();
            }

            //if (UserProfile.Length > 81 && !_CollectedUsersSettings.Users().Contains(UserId))
            //{

            //    //List<string> newUser = Common.Users() as List<string>;
            //    //newUser.Add(UserId);
            //    //Common.UpdateUsers(newUser);
            //    //IList<string> list = Common.Users();
            //    //AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            //    //allowedTypes.AddRange(list.ToArray());
            //    //UsertextBox1.Invoke(new Action(() => { UsertextBox1.AutoCompleteCustomSource = allowedTypes; }));
            //    //UsertextBox1.Invoke(new Action(() => { UsertextBox1.AutoCompleteMode = AutoCompleteMode.Suggest; }));
            //    //UsertextBox1.Invoke(new Action(() => { UsertextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource; }));
            //}




        }

        private string CommandScript()
        {

            StringBuilder sb = new StringBuilder(30);
            sb.Append($"net user {UserId} /DOMAIN");
            return sb.ToString();
        }

    }
}
