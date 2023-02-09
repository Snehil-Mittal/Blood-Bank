using BloodBankManagementSystem.Models;
using BloodBankManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Data
{
    public class UserDetailsUtility
    {
        UserInterfaceForm ui = new UserInterfaceForm();
        public UserDetails CreateUser()
        {
            int userId = ui.GetUserId();
            string userName = ui.GetUserName();
            string bloodGroup = ui.GetBloodGroup();
            int age = ui.GetAge();
            char gender = ui.GetGender();
            string email = ui.GetEmail();
            string location = ui.GetLocation();
           long mobileNo = ui.GetMobileNo();
        UserDetails u = new UserDetails { UserId = userId,UserName=userName,BloodGroup=bloodGroup,Age=age,Gender=gender,Email=email,Location=location,MobileNo=mobileNo };
            return u;
        }
        public void Display(List<UserDetails> list)
        {
            foreach (UserDetails u in list)
            {
                Console.WriteLine(u);
            }
        }
    }
}
