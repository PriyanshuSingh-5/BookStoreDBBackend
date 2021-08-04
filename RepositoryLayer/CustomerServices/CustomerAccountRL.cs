using System;
using System.Data;
using System.Data.SqlClient;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace RepositoryLayer.CustomerServices
{
    public class CustomerAccountRL : ICustomerAccountRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        readonly string InsertCustomerRecordSQL = "InsertCustomerRecord";
        readonly string FetchCustomerRecordSQL = "FetchCustomerLoginRecord";
        readonly string CheckCustomerRecordSQL = "CheckCustomerRecord";
        readonly string ChangeCustomerPasswordSQL = "ChangeCustomerPassword";
        CustomerAccount customer;

        public CustomerAccountRL(IConfiguration config)
        {
            sqlConnectString = config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString;
                ///+ "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        public bool CheckCustomer(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(CheckCustomerRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("Email", forgetPasswordModel.Email);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(1))
                {
                    return true;
                }
                return false;
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

        public CustomerAccount LoginCustomer(LoginCustomerAccount loginCustomerAccount)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(FetchCustomerRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("Email", loginCustomerAccount.Email);
                cmd.Parameters.AddWithValue("Password", loginCustomerAccount.Password);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                customer = new CustomerAccount();
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
         
                if (result != null && result.Equals(2))
                {
                    throw new Exception("Email not registered");
                }
                if (result != null && result.Equals(3))
                {
                    throw new Exception("wrong password");
                }
                if (rd.Read())
                {
                    customer.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    customer.FirstName = rd["CustomerFirstName"] == DBNull.Value ? default : rd.GetString("CustomerFirstName");
                    customer.LastName = rd["CustomerLastName"] == DBNull.Value ? default : rd.GetString("CustomerLastName");
                    customer.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                    customer.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                }
                return customer;
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

        public CustomerAccount RegisterCustomer(RegisterCustomerAccount registerCustomerAccount)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InsertCustomerRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("FirstName", registerCustomerAccount.FirstName);
                cmd.Parameters.AddWithValue("LastName", registerCustomerAccount.LastName);
                cmd.Parameters.AddWithValue("Email", registerCustomerAccount.Email);
                cmd.Parameters.AddWithValue("PhoneNumber", registerCustomerAccount.PhoneNumber);
                cmd.Parameters.AddWithValue("Password", registerCustomerAccount.Password);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                CustomerAccount customer = new CustomerAccount();
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(2))
                {
                    throw new Exception("Email already registered");
                }
                if (rd.Read())
                {
                    customer.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    customer.FirstName = rd["CustomerFirstName"] == DBNull.Value ? default : rd.GetString("CustomerFirstName");
                    customer.LastName = rd["CustomerLastName"] == DBNull.Value ? default : rd.GetString("CustomerLastName");
                    customer.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                    customer.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                }

                return customer;
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

        public bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(ChangeCustomerPasswordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("Email", resetPasswordModel.Email);
                cmd.Parameters.AddWithValue("NewPassword", resetPasswordModel.NewPassword);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                CustomerAccount customer = new CustomerAccount();
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;

                if (result != null && result.Equals(1))
                {
                    return true;
                }
                return false;
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
