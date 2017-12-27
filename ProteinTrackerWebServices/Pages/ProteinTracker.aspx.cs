#region Includes
using ProteinTrackerWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

namespace ProteinTrackerWebServices.Pages
{

    /// <summary>
    ///     If we don't have ASMX Web Serices in your project, or you just have some simple AJAX call 
    ///     if you wanna have JavaScript Available for you, and then all of your methods will be accessible from PageMethods. the name of the method
    /// </summary>
    public partial class ProteinTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]       
        public static int AddProtein(int amount, int userId)
        {
            UserRepository repo = new UserRepository();
            // using the repository, our implementation becomes a little bit simpler.
            var user = repo.GetById(userId);
            if (user == null) return -1;
            user.Total += amount;
            repo.Save(user);
            return user.Total;           
        }

        [WebMethod]
        public static int AddUser(string name, int goal)
        {
            UserRepository repo = new UserRepository();
            // using the User Repository
            var user = new User { Goal = goal, Name = name, Total = 0 }; // for the user ID we're actually NOT going to set because we're going to let the repository handle that. 
                                                                         // We've moved the responsibility of managing the IDs down one layer.
            repo.Add(user);                                              // this is going to fill in the ID for us.
            return user.UserId;
        }

        [WebMethod]
        public static List<User> ListUsers()
        {
            UserRepository repo = new UserRepository();
            // using the User Repository
            return new List<User>(repo.GetAll());
        }
    }
}