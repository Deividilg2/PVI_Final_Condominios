using DataModels;
using PVI_Final_Condominios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace PVI_Final_Condominios.Controllers
{
    public class DetalleCobrosController : Controller
    {
        [HttpGet]
        public ActionResult DetalleCobros(int? id)
        {

            var DetallecobrosList = new List<DetalleCobrosModels>();
            var list = new List<SpConsultarDetalleCobroResult>();//Variable para almacenar los datos del SP de los cobros
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {

                    list = db.SpConsultarDetalleCobro(id).ToList();//Almacenamos el resultado, del SP

                }
            }
            catch
            {

            }

            return View(list);//Pasamos la lista del modelo
           
        }
    }
}