using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProteinTrackerWebServices.Models
{
    public class User
    {
        public string Name { get; set; }
        public int Goal { get; set; }
        public int Total { get; set; }
        public int UserId { get; set; }
    }
}