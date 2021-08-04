using System;
using System.Data;
using System.Data.SqlClient;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AdminInterfaces;

namespace RepositoryLayer.AdminServices
{
    public class AdminAccountRL : IAdminAccountRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        private readonly IConfiguration config;
        readonly string FetchAdminLoginRecordSQL = "FetchAdminLoginRecord";

        public AdminAccountRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString; 
                //+ "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        /// <summary>
        /// Admin
        /// </summary>
        /// <param name="loginAdminAccount"></param>
        /// <returns></returns>
        public AdminAccount LoginAdmin(LoginAdminAccount loginAdminAccount)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(FetchAdminLoginRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("AdminID", loginAdminAccount.AdminID);
                cmd.Parameters.AddWithValue("Password", loginAdminAccount.Password);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                AdminAccount adminAccount = new AdminAccount();
                if (rd.Read())
                {
                    adminAccount.AdminID = rd["AdminID"] == DBNull.Value ? default : rd.GetString("AdminID");
                    adminAccount.AdminName = rd["AdminName"] == DBNull.Value ? default : rd.GetString("AdminName");
                    adminAccount.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                    adminAccount.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                }
                var result = returnParameter.Value;
                if (result != null && result.Equals(2))
                {
                    throw new Exception("AdminID is invalid");
                }
                if (result != null && result.Equals(3))
                {
                    throw new Exception("wrong password");
                }
                return adminAccount;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
