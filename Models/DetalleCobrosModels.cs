using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_Final_Condominios.Models
{
    public class DetalleCobrosModels
    {
        public int id_servicio { get; set; }
        public string nombre_servicio { get; set; }
        public decimal precio_servicio { get; set; }
        public int id_cobro { get; set; }
        public decimal monto_cobro { get; set; }
        public string estado_cobro { get; set; }
        public string nombre_casa { get; set; }
    }
}