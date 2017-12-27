#region Includes
using System;
using System.Collections.Generic;
using System.Web.Services;
using ProteinTrackerWebServices.Models;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Threading;
#endregion

namespace ProteinTrackerWebServices
{
    /// <summary>
    /// We need to inherit from WebService if wanna be able to use some functionality such as get access to app object for na HTTP request or even the session objects that we can have session state
    /// you can get context, server, common things
    /// </summary>
    [WebService(Namespace = "http://connect.greenspoonmarder.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.  [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class ProteinTrackingService : WebService
    {
        // Authentication class will inherit from SOAP header.
        public class AuthenticationHeader : SoapHeader
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public AuthenticationHeader Authentication;  //public field oun our class

        //Create an instance of UserRepository.
        private UserRepository repo = new UserRepository();

        //when this web service is called, it is going to look in the SOAP header that matches the SoapHeader Authentication, it'll look for this property to be filled out
        [WebMethod(Description = "Add an amount to the total.")] //, EnableSession = true)]
        //[SoapHeader("Authentication")]
        public int AddProtein(int amount, int userId)
        {
            //Thread.Sleep(3000); //3 seconds.

            //if (Authentication == null || Authentication.UserName != "Bob" || Authentication.Password != "Pass")
            //    throw new Exception("Bad Credentials!");

            // using the repository, our implementation becomes a little bit simpler.
            var user = repo.GetById(userId);
            if (user == null) return -1;
            user.Total += amount;
            repo.Save(user);
            return user.Total;
            #region Comments
            //if (Session["user" + userId] == null)
            //    return -1; // we can't actually add anything to that user, that user doesn't exist.
            //var user = (User)Session["user" + userId];
            //user.Total += amount;
            //Session["user" + userId] = user;
            //return user.Total;

            //var total = 0;
            //if (Session["total"] != null))
            //    total = (int)Session["total"];
            //total += amount;
            //Session["total"] = total;
            //return total;
            #endregion
        }

        [WebMethod(Description = "Add a user Id.")] //, EnableSession = true)]
        public int AddUser(string name, int goal)
        {
            // using the User Repository
            var user = new User { Goal = goal, Name = name, Total = 0 }; // for the user ID we're actually NOT going to set because we're going to let the repository handle that. 
                                                                         // We've moved the responsibility of managing the IDs down one layer.
            repo.Add(user);                                              // this is going to fill in the ID for us.
            return user.UserId;
            #region comments
            //var userId = 0;
            //if (Session["userId"] != null)
            //    userId = (int)Session["userId"];
            //Session["user" + userId] = new User { Goal = goal, Name = name, Total = 0, UserId = userId };
            //Session["userId"] = userId + 1;
            //return userId;
            #endregion
        }

        [WebMethod(Description = "Add a user to the list of users.")] //, EnableSession = true)]
        public List<User> ListUsers()
        {
            // using the User Repository. We are going to make this into a List because we can't serialize a ReadOnlyCollection
            return new List<User>(repo.GetAll());

            #region Comments
            //var users = new List<User>();   // this is actually going to automatically be serialized for us.
            //var userId = 0;
            //if (Session["userId"] != null)
            //    userId = (int)Session["userId"];
            //for (int i = 0; i < userId; i++)
            //{
            //    users.Add((User)Session["user" + i]);
            //}

            //return users;
            #endregion
        }

    } // end of program.
}
