using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Data
{
    public class UserDetailsDao : IUserDetailsRepository
    {
        SqlConnection conn = null;
        DbHandler dbHandler = new DbHandler();

        public UserDetailsDao(DbHandler dbHandler)
        {
            this.dbHandler = dbHandler;
        }
        public int DeleteUser(int userId)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"delete from UserDetails where user_id = {userId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    Console.WriteLine("Record Deleted Successfully");
                }
                else
                {
                    Console.WriteLine("Enter Valid ID");
                }
                return res;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while deleting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return 0;
        }

        public int InsertUser(UserDetails u)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"insert into UserDetails values ({u.UserId},'{u.UserName}','{u.BloodGroup}',{u.Age},'{u.Gender}','{u.Email}','{u.Location}',{u.MobileNo})";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                return res;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while inerting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return 0;
        }

        public int UpdateUserContactDetails(int userId, long mobile_no, string email)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"update UserDetails set mobile_no = {mobile_no},email_id={email} where user_id = {userId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                return res;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while inerting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return 0;
        }


        public int UpdateUserLocation(int userId, string location)
        {
            try
            {
                conn = dbHandler.OpenConnection();
                string query = $"update UserDetails set location={location} where user_id = {userId}";
                SqlCommand command = new SqlCommand(query, conn);
                int res = command.ExecuteNonQuery();
                return res;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while inerting a record ..." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return 0;
        }


    }
}

