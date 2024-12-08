using DataModels;
using PVI_Final_Condominios.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;

namespace PVI_Final_Condominios.Controllers
{
    public class GestionarCasasController : Controller
    {
        // GET: GestionarCasas
        public ActionResult ConsultarCasas()
        {
            try
            {
                LoginModels usuario = (LoginModels)Session["Usuario"];
                ////Validamos la sesion del usuario activa
                if (Session["Usuario"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else if (usuario.esEmpleado == "Cliente")
                {
                    return RedirectToAction("ConsultarCobros", "Cobros");
                }
            }
            catch 
            {

            }
            
            return View();
        }

        public JsonResult ListaCasas()
        {
            var list = new List<SpConsultarCasasResult>();
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                { 
                list = db.SpConsultarCasas().ToList();
                }
            }
            catch 
            {

            }
            return Json(new {data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrearCasas(int? id)
        
        {//Creacion de una variable cobro con el modelo
            var casa = new GestionarCasasModels();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //Creamos una instancia de Usuario para tomar los datos  del usuario
            LoginModels usuario = (LoginModels)Session["Usuario"];
            if (usuario.esEmpleado == "Cliente")//Validamos que el usuario sea un empleado para poder entrar
            {
                return RedirectToAction("ConsultarCobros", "Cobros");
            }
            try
            {//Conexion a la base de datps
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Cargamos en la variable con la estructura del modelo la consulta de los datos del cobro especifico
                    casa = db.SpConsultarCasaExistente(null, id).Select(_ => new GestionarCasasModels
                    {//Mapeamos los datos al modelo
                        idCasa = _.Id_casa,
                        nombreCasa = _.Nombre_casa,
                        metrosCuadrados = _.Metros_cuadrados,
                        numeroHabitaciones = _.Numero_habitaciones,
                        numeroBannos = _.Numero_banos,
                        idPersona = _.Id_persona,
                        fechaConstruccion = DateTime.Parse(_.Fecha_construccion.ToString("dd-MM-yyyy")),
                        estadocobro = (_.Estadodecobros=="Pendiente"),
                        estado = (_.Estado==true)
                    }).FirstOrDefault();//Especificamos que nos devuelva el primer resultado que deberia ser el unico
                    if (casa == null)//La casa no existe
                    {
                        return RedirectToAction("Index", "Resultados", new { source = "BuscarCasaM" });
                    }else if ( casa.estadocobro == true)//La casa tiene cobros pendientes
                    {
                        return RedirectToAction("Index", "Resultados", new { source = "CasaCobroPendienteM" });
                    }else if (casa.estado == false)
                    {
                        return RedirectToAction("Index", "Resultados", new { source = "CasaInactivaM" });
                    }
                    

                }
            }
            catch
            {

            }
            return View(casa);
        }

        [HttpPost]
        public JsonResult CrearCasas(GestionarCasasModels casa)
        {
            string resultado = String.Empty;
            try
            {
                using(var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    if (casa.idCasa == 0)
                    {
                        var nombreduplicado = db.SpConsultarCasaExistente(casa.nombreCasa, null).FirstOrDefault();
                        if(nombreduplicado == null)
                        {
                            db.SpInsertarCasa(casa.nombreCasa, casa.metrosCuadrados, casa.numeroHabitaciones,
                            casa.numeroBannos, PrecioCasa(casa), casa.idPersona, casa.fechaConstruccion);
                            resultado = "Se ha logrado guardar con éxito";
                        }
                        else
                        {
                            resultado = "Ya existe una casa con ese nombre";
                        }
                    } else
                    {
                        db.SpModificarCasa(casa.idCasa,casa.numeroHabitaciones,
                        casa.numeroBannos, PrecioCasa(casa));
                        resultado = "Se ha logrado modificar con éxito";
                    }
                }
            }
            catch
            {
            }
            return Json(resultado);
        }

        [HttpPost]
        public JsonResult InactivarCasa(int idcasa)
        {
            string resultado = String.Empty;
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    db.SpInactivarCasa(idcasa);
                    resultado = "Casa inactivada con éxito";
                }
            }
            catch
            {
                resultado = "No se ha logrado inactivar la casa";
            }
            return Json(resultado);
        }

        public decimal PrecioCasa(GestionarCasasModels casa)
        {
            decimal precio = 0;
            DateTime anno = casa.fechaConstruccion;
            try
            {
                if(anno.Year >= 1970 && anno.Year <= 1989)
                {
                    precio = 20000000 + (casa.metrosCuadrados * 512) + (casa.numeroHabitaciones * 2000000) + (casa.numeroBannos * 500000);
                } else if (anno.Year >= 1990 && anno.Year <= 2009)
                {
                    precio = 30000000 + (casa.metrosCuadrados * 512) + (casa.numeroHabitaciones * 2000000) + (casa.numeroBannos * 500000);
                } else
                {
                    precio = 40000000 + (casa.metrosCuadrados * 512) + (casa.numeroHabitaciones * 2000000) + (casa.numeroBannos * 500000);
                }

            }
            catch
            {

            }

            return precio;
        }

        public JsonResult DdlClientes()
        {//Ddl que usamos para cargar a los clientes activos
            LoginModels usuario = Session["Usuario"] as LoginModels;
            var list = new List<DropDownList>();//Variable que va a guardar la estructura del modelo DDL
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Almacenamos en list los id y nombres de las personas para el ddl de la vista
                    //Cargamos con normalidad todos los clientes activos
                    list = db.SpConsultarPersona(true).Select(_ => new DropDownList { Id = _.IdPersona, Nombre = _.Nombre + " " + _.Apellido }).ToList();
                }
            }
            catch
            {

            }
            return Json(list);
        }


    }
}