using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.CutomerInterfaces
{
    public interface ICustomerAccountRL
    {
        public CustomerAccount RegisterCustomer(RegisterCustomerAccount registerCustomerAccount);
        CustomerAccount LoginCustomer(LoginCustomerAccount loginCustomerAccount);
        bool CheckCustomer(ForgetPasswordModel forgetPasswordModel);
        bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel);
    }
}
