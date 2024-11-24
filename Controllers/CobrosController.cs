using DataModels;
using Microsoft.Ajax.Utilities;
using PVI_Final_Condominios.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;

namespace PVI_Final_Condominios.Controllers
{
    public class CobrosController : Controller
    {
        // GET: Cobros
        public ActionResult ConsultarCobros()
        {
            //Creamos una variable para almacenar los atributos del modelo
            var list = new List<SpConsultarCobrosResult>();//Variable para almacenar los datos del SP de los empleados
            var cobro = new CobrosModels();
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {
                    ////Validamos la sesion del usuario activa
                    //if (Session["Usuario"] == null)
                    //{
                    //    Response.Redirect("~/Pages/Login.aspx");
                    //}
                    ////Creamos una instancia de Usuario para tomar los datos  del usuario
                    //LoginModels usuario = (LoginModels)Session["Usuario"];
                    //if (usuario.esEmpleado == "Cliente")//Validamos que el usuario sea un empleado para poder entrar
                    //{
                    //    Response.Redirect("~/Cobros/CrearCobros", false);
                    //}

                    list = db.SpConsultarCobros().ToList();//Almacenamos el resultado, del SP
                    ServiciosyddlsdeFechas(cobro);
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
            //if (Session["Usuario"] == null)
            //{
            //    Response.Redirect("~/Pages/Login.aspx");
            //}
            ////Creamos una instancia de Usuario para tomar los datos  del usuario
            //LoginModels usuario = (LoginModels)Session["Usuario"];
            //if (usuario.esEmpleado == "Cliente")//Validamos que el usuario sea un empleado para poder entrar
            //{
            //    Response.Redirect("~/Cobros/CrearCobros", false);
            //}
            try
            {//Conexion a la base de datps
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Cargamos en la variable con la estructura del modelo la consulta de los datos del cobro especifico
                    cobro = db.SpConsultarCobroporId(id).Select(_ => new CobrosModels
                    {//Mapeamos los datos al modelo
                        idcobro = _.Id_cobro,
                        idCliente = _.Id_persona,
                        idcasa = _.Id_casa,
                        mes = _.Mes,
                        anno = _.Anno,
                        estado = _.Estado
                    }).FirstOrDefault();//Especificamos que nos devuelva el primer resultado que deberia ser el unico
                    ServiciosyddlsdeFechas(cobro);//Metodo que nos permite cargar la lista de los años, meses y los servicios en los Checkbox

                }
            }
            catch
            {

            }
            return View(cobro);
        }

         

        [HttpPost]
        public JsonResult CrearCobros(CobrosModels cobro, List<int> servicioSeleccionado)
        {
            string resultado = String.Empty;

            try
            {//Conexion a la base de datps
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Buscamos que no exista nningun registro de una casa con estado "Pendiente"
                    var estadoCasa = db.SpConsultarEstadoCasaporPersona(cobro.idcasa,cobro.mes,cobro.anno).FirstOrDefault();
                    ViewBag.Estadocasa = estadoCasa;//Almacenamos dentro del ViewBag para pasarlo a la alerta en la vista
                    //Validamos que se inserte solo en el caso de que no existan registros pendientes en la casa seleccionada
                    if (cobro.idcobro == 0 && estadoCasa == null)
                    {
                       
                        db.SpInsertarCobro(cobro.idcasa, cobro.mes, cobro.anno, MontoServicios(servicioSeleccionado, cobro));//Tomamos el monto de una funcion
                        InsertarServiciosCobro(servicioSeleccionado, cobro, MontoServicios(servicioSeleccionado, cobro));//Realizamos el insert de los servicios en la tabla detallecobroos
                        resultado = "Se ha logrado guardar con exito";

                    }
                    else if(cobro.idcobro != 0)
                    {
                        db.SpModificarCobro(cobro.idcobro, MontoServicios(servicioSeleccionado, cobro));//Modificamos unicamente el monto de los servicios
                        InsertarServiciosCobro(servicioSeleccionado, cobro, MontoServicios(servicioSeleccionado, cobro));//Realizamos el insert de los servicios en la tabla detallecobroos
                        ViewBag.Estadocasa = estadoCasa = null;//Actualizamos para que la alerta en la vista no salte
                        resultado = "Se ha logrado modificar con exito";
                    }
                    else
                    {
                        resultado = "Ya se encuentra un registro en la casa seleccionada";
                    }
                }
                ServiciosyddlsdeFechas(cobro);//Cargamos los servicios y los ddl de anno y mes

            }
            catch
            {
                resultado = "No se ha logrado guardar";
            }
            return Json(resultado);
        }

        
        

        public JsonResult DdlClientes()
        {//Ddl que usamos para cargar a los clientes activos
            var list = new List<DropDownList>();//Variable que va a guardar la estructura del modelo DDL
            try
            {
                using(var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Almacenamos en list los id y nombres de las personas para el ddl de la vista
                    list = db.SpConsultarPersona().Select(_ => new DropDownList { Id = _.IdPersona, Nombre = _.Nombre }).ToList();         
                }
            }
            catch
            {

            }
                return Json(list);
        }

        public JsonResult DdlCasas(int? Id)
        {//Ddl que usamos para traer las casas activas de un cliente especifico
            var list = new List<DropDownList>();//Variable que va a guardar la estructura del modelo DDL
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Almacenamos en list los id y nombres de las casas para el ddl de la vista
                    list = db.SpConsultarCasaddl(Id).Select(_ => new DropDownList { Id = _.Id_casa, Nombre = _.Nombre_casa }).ToList();
                }
            }
            catch
            {

            }
            return Json(list);
        }

        public JsonResult DdlAnnos(int? anno)
        {
            var list = new List<DropDownList>();
            try
            {
                // Genera la lista de años desde el actual hasta dentro de 10 años
                list = Enumerable.Range(DateTime.Now.Year, 11)
                    .Select(a => new DropDownList
                    {
                        Id = a,             // Usa el año como el ID
                        Nombre = a.ToString() // Convierte el año en cadena para mostrarlo
                    })
                    .ToList();
            }
            catch
            {
            }
            return Json(list);
        }

        public JsonResult DdlMeses(int? mes)
        {
            var list = new List<DropDownList>();
            var textInfo = new System.Globalization.CultureInfo("es-ES", false).TextInfo;
            try
            {
                list = Enumerable.Range(1, 12).Select(m => new DropDownList
                {
                    Id = m, // El valor del mes (numérico)
                    Nombre = textInfo.ToTitleCase(new DateTime(1, m, 1).ToString("MMMM")) // Primera letra del mes en mayuscula, obteniendo la cultura es-ES 
                }).ToList();
            }
            catch
            {
            }
            return Json(list);
        }

        public decimal MontoServicios(List<int> servicioSeleccionado, CobrosModels cobro)
        {
            decimal monto = 0;
            decimal serviciomonto = 0;
            try
            {

                if (servicioSeleccionado != null && servicioSeleccionado.Any())
                {
                    using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD, por fuera del ciclo para evitar la repeticion de una conexion a la BD
                    {
                        var datoscasa = db.SpConsultarCasaddl(cobro.idcasa).FirstOrDefault();
                        foreach (var servicioId in servicioSeleccionado)
                        {
                            var valor = db.SpConsultarPrecioServicioporId(servicioId).FirstOrDefault();
                            serviciomonto += valor.Precio;
                        }
                        monto = (datoscasa.Precio / datoscasa.Metros_cuadrados) + serviciomonto;
                    }
                }

            }
            catch
            {
            }
            return monto;
        }

        public void InsertarServiciosCobro(List<int> servicioSeleccionado, CobrosModels cobro, decimal monto)
        {
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {

                    if (cobro != null)//Si contiene el id de un servicio que no lo tome encuenta
                    {
                        var serviciosexistentes = db.SpConsultarServiciosporCobro(cobro.idcobro).Select(s => s.Id_servicio).ToList();
                        var serviciosInsertar = servicioSeleccionado.Except(serviciosexistentes).ToList();
                        //Comparamos las listas con Except, la lista servicioSeleccionado elimina los que si encuentra en la lista serviciosexistentes
                        var servicioaEliminar = serviciosexistentes.Except(servicioSeleccionado).ToList();//Tomamos los servicios que ya no estan seleccionados
                        foreach (var servicioId in serviciosInsertar)
                        {
                            db.SpInsertarDetalleCobro(servicioId, cobro.idcobro, cobro.idcasa, cobro.mes, cobro.anno, monto);
                        }
                        foreach (var servicioId in servicioaEliminar)
                        {
                            db.SpEliminarServiciodeCobro(servicioId);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public void ServiciosyddlsdeFechas(CobrosModels cobro)
        {

            using (var db = new PviProyectoFinalDB("MyDatabase"))
            {
                ViewBag.servicios = db.SpConsultarServicioscbx().ToList();
                ViewBag.serviciosSeleccionados = cobro != null ? //En caso de que se este insertando un nuevo cobro no genere un error
                    db.SpConsultarServiciosporCobro(cobro.idcobro).Select(s => s.Id_servicio).ToList() : null;
            }
        }


    }
}