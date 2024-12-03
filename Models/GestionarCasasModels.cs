using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVI_Final_Condominios.Models
{
    public class GestionarCasasModels
    {
        public int idCasa {  get; set; }
        public string nombreCasa {  get; set; }
        public int metrosCuadrados {  get; set; }
        public int numeroHabitaciones { get; set; }
        public int numeroBannos { get; set; }
        public int idPersona { get; set; }
        public DateTime fechaConstruccion { get; set; }
        public bool estado { get; set; }

    }
}