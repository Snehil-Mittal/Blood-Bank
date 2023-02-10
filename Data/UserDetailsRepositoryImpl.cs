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

        public Task<UserDetails> GetUserByBloodGroup(string bloodGroup)
        {
            UserDetails u = null;
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
                        u = new UserDetails();
                        while (reader.Read())
                        {
                            u.UserId = reader.GetInt32(0);
                            u.UserName = (string)reader["user_Name"];
                            u.Age = Convert.ToInt32(reader["age"]);
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
                Console.WriteLine("Error while fetching product ..." + ex.Message);
                throw ex;
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
                            u.Age = Convert.ToInt32(reader["age"]);
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
                Console.WriteLine("Error while fetching product ..." + ex.Message);
                throw ex;
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
                            u.Age = Convert.ToInt32(reader["age"]);
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
