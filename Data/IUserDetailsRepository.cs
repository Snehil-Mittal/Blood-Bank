using BloodBankManagementSystem.Dtos;
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
        Task<ReadUserDto> GetUserById(int id);
        Task<IEnumerable<ReadUserDto>> GetUsers();
        Task<ReadUserDto> GetUserByBloodGroup(string bloodGroup);
    }
}
