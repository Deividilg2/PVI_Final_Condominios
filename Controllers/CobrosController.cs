using DataModels;
using PVI_Final_Condominios.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace PVI_Final_Condominios.Controllers
{
    public class CobrosController : Controller
    {
        // GET: Cobros
        public ActionResult ConsultarCobros()
        {
            //Creamos una variable para almacenar los atributos del modelo
            var cobrosList = new List<CobrosModels>();
            var list = new List<SpConsultarCobrosResult>();//Variable para almacenar los datos del SP de los empleados
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {

                    list = db.SpConsultarCobros().ToList();//Almacenamos el resultado, del SP

                }
            }
            catch
            {

            }

            return View(list);//Pasamos la lista del modelo
        }
        
        //Creamos este metodo para poder realizar la carga en caso de modificacion de datos
        public ActionResult CrearCobros(int? id)
        {//Creacion de una variable cobro con el modelo
            var cobro = new CobrosModels();
            try
            {//Conexion a la base de datps
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Cargamos en la variable con la estructura del modelo la consulta de los datos del cobro especifico
                    cobro = db.SpConsultarCobroporId(id).Select(_ => new CobrosModels
                    {//Mapeamos los datos al modelo
                        idcobro = _.Id_cobro,
                        idcasa = _.Id_casa,
                        mes = _.Mes,
                        anno = _.Anno,
                        estado = _.Estado,
                        monto = (decimal)_.Monto,
                        idCliente = _.Id_persona,
                        nombreCliente = _.Nombre,

                    }).FirstOrDefault();//Especificamos que nos devuelva el primer resultado que deberia ser el unico
                   
                    ViewBag.servicios = db.SpConsultarServicioscbx().ToList();
                    Fechas(cobro);

                }
            }
            catch
            {
            }
            return View(cobro);
        }

        public void Fechas(CobrosModels cobro)
        {
            //Creamos un Enumerable y le generamos una secuencia con un rango de numeros enteros, empieza en 2024 contandolo y genera 11 numeros.
            ViewBag.Anno = Enumerable.Range(2024, 11).Select(a => new SelectListItem//Toma cada numero y lo vuelve un elemento individual de la lista 
            {
                Value = a.ToString(),//Convertimos el año "a" en string ya que el ddl solo admite string y lo asignamos como el valor
                Text = a.ToString(),//Convierte el numero a una cadena para mostrarlo al usuario tambien
                Selected = (cobro != null && a == cobro.anno)
                //Solo en caso de que el año se encuentre en la lista creada y coincida con el año que viene de la bd se seleccionara
            }).OrderBy(a => a.Text).ToList();//Ordenamos de forma ascendente y lo volvemos una lista


            //Creacion de la lista de meses
            // Crear la lista de meses
            ViewBag.meses = new List<SelectListItem>
                    {

                        new SelectListItem { Value = "1", Text = "Enero" },
                        new SelectListItem { Value = "2", Text = "Febrero" },
                        new SelectListItem { Value = "3", Text = "Marzo" },
                        new SelectListItem { Value = "4", Text = "Abril" },
                        new SelectListItem { Value = "5", Text = "Mayo" },
                        new SelectListItem { Value = "6", Text = "Junio" },
                        new SelectListItem { Value = "7", Text = "Julio" },
                        new SelectListItem { Value = "8", Text = "Agosto" },
                        new SelectListItem { Value = "9", Text = "Septiembre" },
                        new SelectListItem { Value = "10", Text = "Octubre" },
                        new SelectListItem { Value = "11", Text = "Noviembre" },
                        new SelectListItem { Value = "12", Text = "Diciembre" }
                    };
        }

        public ActionResult ModificarCobros(CobrosModels cobro)
        {
            string resultado = String.Empty;

            try
            {//Conexion a la base de datps
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {
                    if (cobro.idcobro == 0)
                    {
                        db.SpInsertarCobro(cobro.idcasa, cobro.mes, cobro.anno, cobro.estado, cobro.monto);
                        resultado = "Se ha logrado guardar con exito";
                    }
                    else
                    {
                        db.SpModificarCobro(cobro.idcobro, cobro.idcasa, cobro.mes, cobro.anno, cobro.estado, cobro.monto);

                        resultado = "Se ha logrado modificar con exito";
                    }
                }
            }
            catch
            {
            }
            return View();
        }

        public JsonResult DdlClientes()
        {//Ddl que usamos para cargar a los clientes activos
            var list = new List<DropDownList>();//Variable que va a guardar la estructura del modelo DDL
            try
            {
                using(var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Almacenamos en list los id y nombres de las personas
                    list = db.SpConsultarPersona().Select(_ => new DropDownList { Id = _.IdPersona, Nombre = _.Nombre }).ToList();         
                }
            }
            catch
            {

            }
                return Json(list);
        }

        public JsonResult DdlCasas(int? id)
        {//Ddl que usamos para traer las casas activas de un cliente especifico
            var list = new List<DropDownList>();//Variable que va a guardar la estructura del modelo DDL
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Almacenamos en list los id y nombres de las casas
                    list = db.SpConsultarCasaddl(id).Select(_ => new DropDownList { Id = _.Id_casa, Nombre = _.Nombre_casa }).ToList();
                }
            }
            catch
            {

            }
            return Json(list);
        }


    }
}