using DataModels;
using PVI_Final_Condominios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataModels.PviProyectoFinalDBStoredProcedures;

namespace PVI_Final_Condominios.Controllers
{
    public class ServiciosController : Controller
    {
        // GET: Servicios
        public ActionResult ConsultarServicios()
        {
            ////Validamos la sesion del usuario activa
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        public JsonResult ListaServicios()
        {
            var list = new List<SpConsultarServiciosResult>();
            var cobro = new CobrosModels();
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {
                    //Creamos una instancia de Usuario para tomar los datos  del usuario
                    LoginModels usuario = (LoginModels)Session["Usuario"];
                    list = db.SpConsultarServicios().ToList();//Almacenamos el resultado, del SP
                    
                }
            }
            catch
            {

            }
            return Json(new {data = list}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrearServicios(int? id)
        {//Creacion de una variable cobro con el modelo
            var servicio = new ServiciosModels();
            LoginModels usuario = (LoginModels)Session["Usuario"];
            //Creamos una instancia de Usuario para tomar los datos  del usuario

            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            try
            {//Conexion a la base de datps
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Cargamos en la variable con la estructura del modelo la consulta de los datos del cobro especifico
                    servicio = db.SpConsultarServicioporId(id).Select(_ => new ServiciosModels
                    {//Mapeamos los datos al modelo
                        nombre = _.Nombre,
                        descripcion = _.Descripcion,
                        precio= _.Precio,
                        idcategoria = _.Id_categoria,
                        idservicio = _.Id_servicio,
                        estado = _.Estado
                    }).FirstOrDefault();//Especificamos que nos devuelva el primer resultado que deberia ser el unico
                    if (servicio == null)
                    {
                        return RedirectToAction("Index", "Resultados", new { source = "SevicioModificarM" });
                    }
                    var estadopendiente = db.SpConsultarServicioDuplicado(servicio.nombre, servicio.idcategoria,servicio.idservicio).FirstOrDefault();
                    if(estadopendiente != null)
                    {
                        return RedirectToAction("Index", "Resultados", new { source = "ServicioPendienteM" });
                    }
                    
                }
            }
            catch
            {

            }
            return View(servicio);
        }
        [HttpPost]
        public JsonResult CrearServicios (ServiciosModels servicio)
        {//Creacion de una variable cobro con el modelo
            string resultado = String.Empty;
            
            try
            {//Conexion a la base de datps
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    if(servicio.idservicio == 0)
                    {
                        var nombreduplicado = db.SpConsultarServicioDuplicado(servicio.nombre, servicio.idcategoria, null).FirstOrDefault();
                        if (nombreduplicado == null)
                        {
                            db.SpInsertarServicio(servicio.nombre, servicio.descripcion, servicio.precio, servicio.idcategoria);
                            resultado = "El servicio se ha creado con éxito";
                        }
                        else
                        {
                            resultado = "Ya existe este servicio en la misma categoría ";
                        }
                    }
                    else
                    {
                        db.SpModificarServicio(servicio.idservicio,servicio.descripcion,servicio.precio, null);
                        resultado = "Se ha logrado modificar el servicio con éxito";
                    }
                }
            }
            catch
            {
                resultado = "Se ha producido un fallo";
            }
            return Json(resultado);
        }

        [HttpPost]
        public JsonResult InactivarServicio(ServiciosModels servicio)
        {
            string resultado = String.Empty;
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))
                {
                    db.SpModificarServicio(servicio.idservicio, null,null,false);
                    resultado = "Servicio inactivado con éxito";
                }
            }
            catch
            {
                resultado = "No se ha logrado inactivar el servicio";
            }
            return Json(resultado);
        }

        public JsonResult DdlCategorias()
        {//Ddl que usamos para traer las casas activas de un cliente especifico
            var list = new List<DropDownList>();//Variable que va a guardar la estructura del modelo DDL
            try
            {
                using (var db = new PviProyectoFinalDB("MyDatabase"))//Using para realizar la conexion con la BD
                {//Almacenamos en list los id y nombres de las casas para el ddl de la vista
                    list = db.SpConsultarCategorias().Select(_ => new DropDownList { Id = _.IdCategoria, Nombre = _.Nombre }).ToList();
                }
            }
            catch
            {

            }
            return Json(list);
        }
    }
}