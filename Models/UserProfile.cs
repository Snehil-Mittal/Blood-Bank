using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public bool Availability { get; set; }
        public string Badge { get; set; }
        public string Role { get; set; }
        public bool IsApproved { get; set; }
        public DateTime LastDonated { get; set; }

    }
}
