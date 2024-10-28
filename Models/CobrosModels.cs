using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_Final_Condominios.Models
{
    public class CobrosModels
    {
        public int idcobro { get; set; }
        public int idcasa { get; set; }
        public int mes { get; set; }
        public int anno { get; set; }
        public string estado { get; set; }
        public decimal monto { get; set; }


        public int idCliente { get; set; }
        public string nombreCliente { get; set; }

        //Atributos para almacenar los checkbox de servicios
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }
}