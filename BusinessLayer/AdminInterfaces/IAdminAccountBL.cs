using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace BusinessLayer.AdminInterfaces
{
    public interface IAdminAccountBL
    {
        AdminAccount LoginAdmin(LoginAdminAccount loginAdminAccount);
    }
}
