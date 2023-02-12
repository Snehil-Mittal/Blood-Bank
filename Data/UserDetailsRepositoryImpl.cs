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
    public class UserDetailsRepositoryImpl : IUserDetailsRepository
    {
        SqlConnection conn = null;
        DbHandler dbHandler = new DbHandler();
        public UserDetailsRepositoryImpl(DbHandler dbHandler)
        {
            this.dbHandler = dbHandler;
        }
        public Task<int> DeleteUser(int userId)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"delete from UserDetails where user_id = {userId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                return Task.Run(() =>
                {
                    return res;
                });
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while deleting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return Task.Run(() =>
            {
                return 0;
            });
        }

        public Task<ReadUserDto> GetUserByBloodGroup(string bloodGroup)
        {
            ReadUserDto u = null;
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
                        u = new ReadUserDto();
                        while (reader.Read())
                        {
                            u.UserName = (string)reader["user_Name"];
                            u.Age = Convert.ToInt32(reader["age"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Email = (string)reader["email"];
                            u.Location = (string)reader["location"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
                            u.Profile.Availability = Convert.ToBoolean(reader["availability"]);
                            u.Profile.Badge = (string)reader["badge"];
                            u.Profile.IsApproved = Convert.ToBoolean(reader["isApproved"]);
                            u.Profile.LastDonated = Convert.ToDateTime(reader["lastDonated"]);
                            u.Profile.Role = (string)(reader["role"]);

                        }
                    }
                    reader.Close();
                }
                return Task.Run(() => u);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while fetching product ..." + ex.Message);
                throw ex;
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<ReadUserDto> GetUserById(int id)
        {
            ReadUserDto u = null;
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"select * from UserDetails ud join UserProfile up on ud.user_id=up.user_id where ud.user_id = @id";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@user_id", id);
                    //Execute the select query
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        u = new ReadUserDto();
                        while (reader.Read())
                        {
                            u.UserName = (string)reader["user_Name"];
                            u.Age = Convert.ToInt32(reader["age"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Email = (string)reader["email"];
                            u.Location = (string)reader["location"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
                            u.Profile.Availability = Convert.ToBoolean(reader["availability"]);
                            u.Profile.Badge = (string)reader["badge"];
                            u.Profile.IsApproved = Convert.ToBoolean(reader["isApproved"]);
                            u.Profile.LastDonated = Convert.ToDateTime(reader["lastDonated"]);
                            u.Profile.Role = (string)(reader["role"]);

                        }
                    }
                    reader.Close();
                }
                return Task.Run(() => u);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while fetching product ..." + ex.Message);
                throw ex;
            }
            finally
            {
                dbHandler.CloseConnection();
            }
        }

        public Task<IEnumerable<ReadUserDto>> GetUsers()
        {
            List<ReadUserDto> userList = new List<ReadUserDto>();
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
                            ReadUserDto u = new ReadUserDto();
                            u.UserName = (string)reader["user_Name"];
                            u.Age = Convert.ToInt32(reader["age"]);
                            u.BloodGroup = (string)reader["blood_group"];
                            u.Email = (string)reader["email"];
                            u.Location = (string)reader["location"];
                            u.Gender = Convert.ToChar(reader["gender"]);
                            u.MobileNo = Convert.ToInt64(reader["mobile_no"]);
                            u.Profile.Availability = Convert.ToBoolean(reader["availability"]);
                            u.Profile.Badge = (string)reader["badge"];
                            u.Profile.IsApproved = Convert.ToBoolean(reader["isApproved"]);
                            u.Profile.LastDonated = Convert.ToDateTime(reader["lastDonated"]);
                            u.Profile.Role = (string)(reader["role"]);

                        }
                    }
                    reader.Close();
                }
                return Task.FromResult<IEnumerable<ReadUserDto>>(userList);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while fetching products ..." + ex.Message);
                throw ex;
            }
        }

        public Task<int> InsertUser(UserDetails u)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"insert into UserDetails values ({u.UserId},'{u.UserName}','{u.BloodGroup}',{u.Age},'{u.Gender}','{u.Email}','{u.Location}',{u.MobileNo})";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                return Task.Run(()=>res);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while inerting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return Task.Run(() => 0);
        }

        public Task<int> UpdateUserContactDetails(int userId, long mobile_no, string email)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"update UserDetails set mobile_no = {mobile_no},email_id={email} where user_id = {userId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                return Task.Run(() => res);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while inerting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return Task.Run(() => 0);
        }

        public Task<int> UpdateUserLocation(int userId, string location)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"update UserDetails set location={location} where user_id = {userId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                return Task.Run(() => res);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while inerting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return Task.Run(() => 0);
        }
    }
}
