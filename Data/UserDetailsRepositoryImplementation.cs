using BloodBankManagementSystem.Dtos;
using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Data
{
    public class UserDetailsRepositoryImplementation : IUserDetailsRepository
    {
        SqlConnection conn = null;
        DbHandler dbHandler;
        public UserDetailsRepositoryImplementation(DbHandler dbHandler)
        {
            this.dbHandler = dbHandler;
        }
        public Task<bool> DeleteUser(int userId)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"Delete from UserDetails where user_id = {userId}";
                SqlCommand command = new SqlCommand(query, conn);

                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    return Task.Run(() => true);
                }
                else
                {
                    return Task.Run(() => false);
                }
                                 
            }
            catch (SqlException ex)
            {
                throw new Exception("Deletion failed" + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<IEnumerable<UserDetails>> GetUsersByBloodGroup(string bloodGroup)
        {
            List<UserDetails> userList = new List<UserDetails>();
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"select * from UserDetails ud join UserProfile up on ud.user_id=up.user_id where blood_group = @bloodGroup";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@bloodGroup", bloodGroup);
                    //Execute the select query
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        
                        while (reader.Read())
                        {
                            UserDetails u = new UserDetails();
                            u.UserName = (string)reader["user_Name"];
                            u.DateOfBirth = Convert.ToDateTime(reader["dob"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Location = (string)reader["location"];
                            u.Email = (string)reader["email"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
                            u.Account.Availability = Convert.ToBoolean(reader["availability"]);
                            u.Account.Badge = (string)reader["badge"];
                            u.Account.IsApproved = Convert.ToBoolean(reader["isApproved"]);
                            u.Account.LastDonated = Convert.ToDateTime(reader["lastDonated"]);
                            u.Account.Role = (string)(reader["role"]);
                            userList.Add(u);
                        }
                    }
                    reader.Close();
                }
                return Task.FromResult<IEnumerable<UserDetails>>(userList); ;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while fetching product ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<UserDetails> GetUserById(int id)
        {
            UserDetails u = null;
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"Select * from UserDetails ud join UserProfile up on ud.user_id = up.user_id where ud.user_id = @id";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    //Execute the select query
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        u = new UserDetails();
                        while (reader.Read())
                        {
                            u.UserName = (string)reader["user_Name"];
                            u.DateOfBirth = Convert.ToDateTime(reader["dob"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Location = (string)reader["location"];
                            u.Email = (string)reader["email"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
                            u.Account.Availability = Convert.ToBoolean(reader["availability"]);
                            u.Account.Badge = (string)reader["badge"];
                            u.Account.IsApproved = Convert.ToBoolean(reader["isApproved"]);
                            u.Account.LastDonated = Convert.ToDateTime(reader["lastDonated"]);
                            u.Account.Role = (string)(reader["role"]);
                        }
                    }
                    reader.Close();
                }
                return Task.Run(() => u);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while fetching product ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<IEnumerable<UserDetails>> GetUsers()
        {
            List<UserDetails> userList = new List<UserDetails>();
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"select * from UserDetails ud join UserProfile up on ud.user_id=up.user_id";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserDetails u = new UserDetails();
                            u.UserName = (string)reader["user_Name"];
                            u.DateOfBirth = Convert.ToDateTime(reader["dob"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Email = (string)reader["email"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.Location = (string)reader["location"];
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
                            u.Account.Availability = Convert.ToBoolean(reader["availability"]);
                            u.Account.Badge = (string)reader["badge"];
                            u.Account.IsApproved = Convert.ToBoolean(reader["isApproved"]);
                            u.Account.LastDonated = Convert.ToDateTime(reader["lastDonated"]);
                            u.Account.Role = (string)(reader["role"]);
                            userList.Add(u);
                        }
                    }
                    reader.Close();
                }
                return Task.FromResult<IEnumerable<UserDetails>>(userList);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error while fetching product ..." + ex.Message);
            }
        }

        public Task<bool> InsertUser(UserDetails u)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                UserHelpUtility ob = new UserHelpUtility();
                ob.AddAccountDetails(u);
                string query1 = $"Insert into UserDetails values ({u.UserId},'{u.UserName}','{u.BloodGroup}',{u.DateOfBirth},'{u.Gender}','{u.Email}','{u.Location}',{u.MobileNo})";
                string query2 = $"Insert into UserAccount values ({u.UserId},{u.Account.Availability},'{u.Account.Badge}','{u.Account.Role}',{u.Account.IsApproved},{u.Account.LastDonated},{u.Account.DonationCount})";
                SqlCommand command = new SqlCommand(query1, conn);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    return Task.Run(() => true);
                }
                else
                {
                    return Task.Run(() => false);
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Insertion failed" + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<bool> UpdateUserDetails(UserDetails u)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"Update UserDetails set mobile_no = {u.MobileNo},email_id={u.Email}, location={u.Location} where user_id = {u.UserId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    return Task.Run(() => true);
                }
                else
                {
                    return Task.Run(() => false);
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Insertion failed" + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<bool> UpdateUserProfile(int id, string role, bool availability)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                UserDetails u = GetUserById(id).Result;
                string query;
                var prevDate = u.Account.LastDonated;
                var today = DateTime.Now;
                var diffOfDates = today - prevDate;
                if (u.Account.LastDonated != null && diffOfDates.Value.TotalDays >= 56)
                {
                    query = $"Update UserAccount set role= {role}, availability={availability} where user_id = {id}";
                }
                else
                {
                    query = $"Update UserAccount set role= {role} availability = {false} where user_id = {id}";
                }
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    return Task.Run(() => true);
                }
                else
                {
                    return Task.Run(() => false);
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Insertion failed" + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<bool> UpdateDonation(UserDetails u, DateTime lastDonated)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                UserHelpUtility ob = new UserHelpUtility();
                ob.UpdateDonationDetails(u, lastDonated);
                string query = $"Update UserAccount set lastDonated = {u.Account.LastDonated}, donationCount = {u.Account.DonationCount}, badge ={u.Account.Badge} where user_id = {u.UserId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    return Task.Run(() => true);
                }
                else
                {
                    return Task.Run(() => false);
                }             

            }
            catch (SqlException ex)
            {
                throw new Exception("Insertion failed" + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<bool> UpdateApproval(UserDetails u)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"Update UserAccount set isApproved = {true}where user_id = {u.UserId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    return Task.Run(() => true);
                }
                else
                {
                    return Task.Run(() => false);
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Approval failed" + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }
    }
}
