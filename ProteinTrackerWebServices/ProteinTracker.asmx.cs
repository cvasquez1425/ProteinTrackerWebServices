#region Includes
using ProteinTrackerWebServices.Models;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
#endregion

namespace ProteinTrackerWebServices
{
    /// <summary>
    /// Create another Web Services file ASMX 
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class ProteinTracker_Test : WebService
    {

        [WebMethod]
        public List<User> ListUsers()
        {
            return new List<User> { new User() { Goal = 100, Name = "Carlos", Total = 50, UserId = 5 } };
        }

    }
}
