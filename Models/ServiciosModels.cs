using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_Final_Condominios.Models
{
    public class ServiciosModels
    {
        public int idservicio {  get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int idcategoria { get; set; }
        public bool estado { get; set; }
    }
}