using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.AdminInterfaces
{
    public interface IAdminAccountRL
    {
        AdminAccount LoginAdmin(LoginAdminAccount loginAdminAccount);
    }
}
