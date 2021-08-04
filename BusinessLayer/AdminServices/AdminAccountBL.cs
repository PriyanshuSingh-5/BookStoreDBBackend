using System;
using BusinessLayer.AdminInterfaces;
using BusinessLayer.CustomerServices;
using BusinessLayer.JWTAuthentication;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AdminInterfaces;

namespace BusinessLayer.AdminServices
{
    public class AdminAccountBL : IAdminAccountBL
    {
        UserAuthenticationJWT AuthJWT;
        private readonly IConfiguration config;
        PasswordEncryption passwordEncryption = new PasswordEncryption();
        IAdminAccountRL adminAccountRL;

        public AdminAccountBL(IAdminAccountRL adminAccountRL, IConfiguration config)
        {
            this.adminAccountRL = adminAccountRL;
            this.config = config;
            AuthJWT = new UserAuthenticationJWT(config);
        }

        public AdminAccount LoginAdmin(LoginAdminAccount loginAdminAccount)
        {
            try
            {
                loginAdminAccount.Password = passwordEncryption.EncryptPassword(loginAdminAccount.Password);
                var result =  adminAccountRL.LoginAdmin(loginAdminAccount);
                if (result != null)
                {
                    result.token = AuthJWT.GenerateAdminSessionJWT(result);
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
