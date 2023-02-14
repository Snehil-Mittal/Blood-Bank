using BloodBankManagementSystem.Dtos;
using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Data
{
    public interface IUserDetailsRepository
    {
        Task<bool> InsertUser(UserDetails u);
        Task<bool> UpdateUserDetails(UserDetails u);
        Task<bool> UpdateUserProfile(int id, string role, bool availability);
        Task<bool> DeleteUser(int userId);
        Task<bool> UpdateApproval(UserDetails u);
        Task<UserDetails> GetUserById(int id);
        Task<bool> UpdateDonation(UserDetails u, DateTime lastDonated);
        Task<IEnumerable<UserDetails>> GetUsers();
        Task<IEnumerable<UserDetails>> GetUsersByBloodGroup(string bloodGroup);
    }
}
