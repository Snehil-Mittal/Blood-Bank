using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Data
{
    interface IUserDetailsRepository
    {
        Task<int> InsertUser(UserDetails u);
        Task<int> UpdateUserContactDetails(int userId, long mobile_no, string email);
        Task<int> UpdateUserLocation(int userId, string location);
        Task<int> DeleteUser(int userId);
        Task<UserDetails> GetUserById(int id);
        Task<IEnumerable<UserDetails>> GetUsers();
        Task<UserDetails> GetUserByBloodGroup(string bloodGroup);
    }
}
