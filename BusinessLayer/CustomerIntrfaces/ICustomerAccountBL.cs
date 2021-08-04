using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace BusinessLayer.CustomerIntrfaces
{
    public interface ICustomerAccountBL
    {
        public CustomerAccount RegisterCustomer(RegisterCustomerAccount registerCustomerAccount);
         public CustomerAccount LoginCustomer(LoginCustomerAccount loginCustomerAccount);
        bool SendForgottenPasswordLink(ForgetPasswordModel forgetPasswordModel);
        bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel);
    }
}
