using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
    [Table("Registrations")] // Replace "YourDatabaseTableName" with your actual table name
    public class Registration
    {
        public int Id { get; set; }       
            public string Name { get; set; }
            public string PhoneNo { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string IsMvcSkill { get; set; } // Yes/No
            public string IsCSharpSkill { get; set; } // Yes/No
            public string IsAspNetSkill { get; set; } // Yes/No
            public string Address { get; set; }       
    }

    public class RegistrationViewModel
    {
        public List<Registration> Registrations { get; set; }
    }


}