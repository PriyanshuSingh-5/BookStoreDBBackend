using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class LoginAdminAccount
    {
        [Required]
        public string AdminID { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
