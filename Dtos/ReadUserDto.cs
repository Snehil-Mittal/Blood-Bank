using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Dtos
{
    public class ReadUserDto { 
    public string UserName { get; set; }
    public string BloodGroup { get; set; }
    public int Age { get; set; }
    public char Gender { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public int MobileNo { get; set; }
    public UserProfile Profile { get; set; }

}
}
