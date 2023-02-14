using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string BloodGroup { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public long MobileNo { get; set; }
        public UserAccount Account { get; set; }


    }
}
