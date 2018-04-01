using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApplication3.Model
{

    //static internal class Common
    //{

    //    /// <summary>
    //    /// Updates or creates Configuration file. bin\AED1351.EXE.config
    //    /// </summary>
    //    /// <remarks></remarks>
    //    public static void CheckConfig()
    //    {
    //        try
    //        {
               
    //            var builder = new ConfigurationBuilder()
    //                          .SetBasePath(Directory.GetCurrentDirectory())
    //                          .AddJsonFile("appsettings.json");

    //            var config = builder.Build();



    //            bool Newconfig = true;
    //            //Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);

    //            //System.IO.FileInfo fileInfo = new System.IO.FileInfo(config.FilePath);
    //            //if (fileInfo.Exists && fileInfo.IsReadOnly == true)
    //            //{
    //            //    fileInfo.IsReadOnly = false;
    //            //}
    //            var test = config.GetSection("CollectedUsersSettings").Value;
    //            var users = config.GetSection("Users").Value;


    //            //if (users == null || users.Count == 0)
    //            //{
    //            //    //config.AppSettings.Settings.Add("users", "xsc7529");
    //            //    Newconfig = true;
    //            //}

    //            //if (Newconfig)
    //            //{
    //            //    //config.Save();
    //            //    //ConfigurationManager.RefreshSection("appSettings");
    //            //}
    //        }
    //        catch (Exception)// e)
    //        {
    //            //TraceWrapper.WriteAudit(string.Concat("Check Config error: ", e.ToString()));

    //        }

    //    }




    //    public static IList<string> Users()
    //    {

    //        var builder = new ConfigurationBuilder()
    //                         .SetBasePath(Directory.GetCurrentDirectory())
    //                         .AddJsonFile("appsettings.json");

    //        var config = builder.Build();
    //        return config.GetSection("Users").Get<string[]>().ToList<string>();

    //    }


    //    public const string DefaultURL = @"http://localhost/BBVA.Cad.API";
    //    /// <summary>
    //    /// Updates LastProcessed of Configuration file.
    //    /// </summary>
    //    /// <remarks></remarks>
    //    public static void UpdateUsers(List<string> list)
    //    {

    //        //try
    //        //{

    //        //    list.Sort();
    //        //    Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
    //        //    System.IO.FileInfo fileInfo = new System.IO.FileInfo(config.FilePath);
    //        //    if (fileInfo.Exists && fileInfo.IsReadOnly == true)
    //        //    {
    //        //        fileInfo.IsReadOnly = false;
    //        //    }

    //        //    if (config.AppSettings.Settings["users"] == null)
    //        //    {
    //        //        config.AppSettings.Settings.Add("users", "xsc7529");
    //        //    }
    //        //    else
    //        //    {
    //        //        config.AppSettings.Settings["users"].Value = string.Join(",", list);
    //        //    }

    //        //    config.Save();

    //        //    ConfigurationManager.RefreshSection("appSettings");

    //        //}
    //        //catch (Exception e)
    //        //{

    //        //    // TraceWrapper.WriteAudit(string.Concat("Update Config error: ", e.ToString()));
    //        //}

    //    }


    //}

    public class CollectedUsersSettings
    {
        public List<string> Users { get; set; }
    }

}
