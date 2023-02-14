using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Dtos
{
    public class UpdateUserDto
    {
        public string Email { get; set; }
        public string Location { get; set; }
        public int MobileNo { get; set; }
        public string Role { get; set; }
        public bool Availability { get; set; }
        public DateTime LastDonated { get; set; }
        
    }
}
