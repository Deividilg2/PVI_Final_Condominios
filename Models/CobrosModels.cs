using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_Final_Condominios.Models
{
    public class CobrosModels
    {
        public int idcobro { get; set; }
        public int idCliente { get; set; }
        public int idcasa { get; set; }
        public int mes { get; set; }
        public int anno { get; set; }
        public string estado { get; set; }

        //Variables necesarias para RF cobro detalle

        public int monto { get; set; }



    }
}