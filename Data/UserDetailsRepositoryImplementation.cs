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
        DbHandler dbHandler = new DbHandler();
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
                string query = $"select * from UserDetails where blood_group = @bloodGroup";
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
                            u.UserId = reader.GetInt32(0);
                            u.UserName = (string)reader["user_Name"];
                            u.DateOfBirth = Convert.ToDateTime(reader["dob"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Email = (string)reader["email"];
                            u.Location = (string)reader["location"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
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
                string query = $"select * from UserDetails where user_id = @id";
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
                            u.UserId = reader.GetInt32(0);
                            u.UserName = (string)reader["user_Name"];
                            u.DateOfBirth = Convert.ToDateTime(reader["dob"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Email = (string)reader["email"];
                            u.Location = (string)reader["location"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
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
                string query = $"select * from UserDetails";
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
                            u.UserId = reader.GetInt32(0);
                            u.UserName = (string)reader["user_Name"];
                            u.DateOfBirth = Convert.ToDateTime(reader["dob"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Email = (string)reader["email"];
                            u.Location = (string)reader["location"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
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
                string query = $"Insert into UserDetails values ({u.UserId},'{u.UserName}','{u.BloodGroup}',{u.DateOfBirth},'{u.Gender}','{u.Email}','{u.Location}',{u.MobileNo})";
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
                string query = $"Update UserAccount set role= {role}, availability={availability} where user_id = {id}";
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
    }
}
