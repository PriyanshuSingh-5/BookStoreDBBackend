using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace RepositoryLayer.CustomerServices
{
    public class CustomerAddressRL : ICustomerAddressRL
    {
        private readonly IConfiguration config;
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        readonly string UpdateCustomerAddressSQL = "UpdateCustomerAddress";
        readonly string InsertCustomerAddressSQL = "InsertCustomerAddress";
        readonly string DeleteCustomerAddressSQL = "DeleteCustomerAddress";
        readonly string GetAllCustomerAddressSQL = "GetAllCustomerAddress";
        CustomerAddressResponse customerAddress;
        public CustomerAddressRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = this.config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString;
                //+ "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        public CustomerAddressResponse AddCustomerAddress(CustomerAddress address)
        {
            try
            {
                customerAddress = new CustomerAddressResponse();
                connection.Open();
                SqlCommand cmd = new SqlCommand(InsertCustomerAddressSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", address.CustomerID);
                cmd.Parameters.AddWithValue("Name", address.Name);
                cmd.Parameters.AddWithValue("Pincode", address.Pincode);
                cmd.Parameters.AddWithValue("PhoneNumber", address.PhoneNumber);
                cmd.Parameters.AddWithValue("Address", address.Address);
                cmd.Parameters.AddWithValue("City", address.City);
                cmd.Parameters.AddWithValue("AddressType", address.AddressType);
                cmd.Parameters.AddWithValue("Landmark", address.Landmark);
                cmd.Parameters.AddWithValue("Locality", address.Locality);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    customerAddress.AddressID = rd["CustomerAddressID"] == DBNull.Value ? default : rd.GetInt64("CustomerAddressID");
                    customerAddress.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                    customerAddress.Address = rd["Address"] == DBNull.Value ? default : rd.GetString("Address");
                    customerAddress.Name = rd["Name"] == DBNull.Value ? default : rd.GetString("Name");
                    customerAddress.AddressType = rd["AddressType"] == DBNull.Value ? default : rd.GetString("AddressType");
                    customerAddress.City = rd["City"] == DBNull.Value ? default : rd.GetString("City");
                    customerAddress.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    customerAddress.Pincode = rd["Pincode"] == DBNull.Value ? default : rd.GetInt32("Pincode");
                    customerAddress.Landmark = rd["Landmark"] == DBNull.Value ? default : rd.GetString("Landmark");
                    customerAddress.Locality = rd["Locality"] == DBNull.Value ? default : rd.GetString("Locality");
                }
                return customerAddress;
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

        public bool DeleteCustomerAddress(string customerID, long addressID)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(DeleteCustomerAddressSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", customerID);
                cmd.Parameters.AddWithValue("AddressID", addressID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(2))
                {
                    throw new Exception("AddressID dont exist");
                }
                return true;
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

        public ICollection<CustomerAddressResponse> GetAllCustomerAddress(string customerID)
        {
            try
            {
                ICollection<CustomerAddressResponse> customerAddresses = new List<CustomerAddressResponse>();
                connection.Open();
                SqlCommand cmd = new SqlCommand(GetAllCustomerAddressSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", customerID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                while (rd.Read())
                {
                    customerAddress = new CustomerAddressResponse();
                    customerAddress.AddressID = rd["CustomerAddressID"] == DBNull.Value ? default : rd.GetInt64("CustomerAddressID");
                    customerAddress.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                    customerAddress.Address = rd["Address"] == DBNull.Value ? default : rd.GetString("Address");
                    customerAddress.Name = rd["Name"] == DBNull.Value ? default : rd.GetString("Name");
                    customerAddress.AddressType = rd["AddressType"] == DBNull.Value ? default : rd.GetString("AddressType");
                    customerAddress.City = rd["City"] == DBNull.Value ? default : rd.GetString("City");
                    customerAddress.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    customerAddress.Pincode = rd["Pincode"] == DBNull.Value ? default : rd.GetInt32("Pincode");
                    customerAddress.Landmark = rd["Landmark"] == DBNull.Value ? default : rd.GetString("Landmark");
                    customerAddress.Locality = rd["Locality"] == DBNull.Value ? default : rd.GetString("Locality");
                    customerAddresses.Add(customerAddress);
                }
                return customerAddresses;
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

        public CustomerAddressResponse UpdateCustomerAddress(CustomerAddress address)
        {
            try
            {
                customerAddress = new CustomerAddressResponse();
                connection.Open();
                SqlCommand cmd = new SqlCommand(UpdateCustomerAddressSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", address.CustomerID);
                cmd.Parameters.AddWithValue("CustomerAddressID", address.CustomerAddressID);
                cmd.Parameters.AddWithValue("Name", address.Name);
                cmd.Parameters.AddWithValue("Pincode", address.Pincode);
                cmd.Parameters.AddWithValue("PhoneNumber", address.PhoneNumber);
                cmd.Parameters.AddWithValue("Address", address.Address);
                cmd.Parameters.AddWithValue("City", address.City);
                cmd.Parameters.AddWithValue("AddressType", address.AddressType);
                cmd.Parameters.AddWithValue("Landmark", address.Landmark);
                cmd.Parameters.AddWithValue("Locality", address.Locality);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    customerAddress.AddressID = rd["CustomerAddressID"] == DBNull.Value ? default : rd.GetInt64("CustomerAddressID");
                    customerAddress.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                    customerAddress.Address = rd["Address"] == DBNull.Value ? default : rd.GetString("Address");
                    customerAddress.Name = rd["Name"] == DBNull.Value ? default : rd.GetString("Name");
                    customerAddress.AddressType = rd["AddressType"] == DBNull.Value ? default : rd.GetString("AddressType");
                    customerAddress.City = rd["City"] == DBNull.Value ? default : rd.GetString("City");
                    customerAddress.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    customerAddress.Pincode = rd["Pincode"] == DBNull.Value ? default : rd.GetInt32("Pincode");
                    customerAddress.Landmark = rd["Landmark"] == DBNull.Value ? default : rd.GetString("Landmark");
                    customerAddress.Locality = rd["Locality"] == DBNull.Value ? default : rd.GetString("Locality");
                }
                return customerAddress;
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
