using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.JWTAuthentication
{
    /// <summary>
    /// JW Token generator
    /// </summary>
    public class UserAuthenticationJWT
    {
        private readonly IConfiguration config;
        SymmetricSecurityKey securityKey;
        SigningCredentials credentials;

        public UserAuthenticationJWT(IConfiguration config)
        {
            this.config = config;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string GenerateCustomerSessionJWT(CustomerAccount userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddDays(10);
            IEnumerable<Claim> Claims = new Claim[] {
                new Claim(ClaimTypes.Role, "Customer"),
                new Claim("CustomerID", userInfo.CustomerID.ToString()),
                new Claim( "Email", userInfo.Email) };
            return GenerateJSONWebToken(ExpireTime, Claims);
        }

        public string GenerateAdminSessionJWT(AdminAccount userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddDays(10);
            IEnumerable<Claim> Claims = new Claim[] {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("AdminID", userInfo.AdminID.ToString()),
                new Claim("Email", userInfo.Email) };
            return GenerateJSONWebToken(ExpireTime, Claims);
        }

        public string GenerateCustomerPasswordResetJWT(ForgetPasswordModel userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddMinutes(120);
            IEnumerable<Claim> Claims = new Claim[] {
                new Claim("UserType", "Customer"),
                new Claim("Email", userInfo.Email) };
            return GeneratePasswordResetJWT(ExpireTime, Claims);
        }

        public string GeneratePasswordResetJWT(DateTime ExpireTime, IEnumerable<Claim> Claims)
        {
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              claims: Claims,
              expires: ExpireTime,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateJSONWebToken(DateTime ExpireTime, IEnumerable<Claim> Claims)
        {
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              claims: Claims,
              expires: ExpireTime,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
