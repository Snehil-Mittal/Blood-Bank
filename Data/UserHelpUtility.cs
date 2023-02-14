using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Data
{
    public class UserHelpUtility
    {
      public void UpdateDonationDetails(UserDetails u,DateTime lastDonated)
        {
            if(u.Account.LastDonated == null)
            {
                u.Account.DonationCount = u.Account.DonationCount + 1;
                u.Account.LastDonated = lastDonated;
                if(u.Account.DonationCount > 5)
                {
                    u.Account.Badge = "Silver";
                }
                if(u.Account.DonationCount > 10)
                {
                    u.Account.Badge = "Gold";
                }
            }
        }

        public void AddAccountDetails(UserDetails u)
        {
            u.Account.Badge = "Welcome";
            u.Account.DonationCount = 0;
            u.Account.IsApproved = false;
            u.Account.LastDonated = null;
            u.Account.Role = null;
            u.Account.Availability = false;
        }
    }
}
