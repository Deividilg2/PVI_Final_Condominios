using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_Final_Condominios.Models
{
    public class LoginModels
    {
        public int id { get; set; }
        public string nombreCompleto { get; set; }
        public string esEmpleado { get; set; }
        public bool Estado { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}