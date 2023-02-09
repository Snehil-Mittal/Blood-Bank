using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Data
{
    interface IUserDetailsRepository
    {
        int InsertUser(UserDetails u);
        int UpdateUserContactDetails(int userId, long mobile_no, string email);
        int UpdateUserLocation(int userId, string location);
        int DeleteUser(int userId);
    }
}
