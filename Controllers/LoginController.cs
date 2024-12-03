using DataModels;
using LinqToDB;
using PVI_Final_Condominios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVI_Final_Condominios.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            try
            {

                if (Session["Usuario"] != null)
                {
                    LoginModels usuario = Session["Usuario"] as LoginModels;
                    if (usuario.esEmpleado == "Empleado")
                    {
                        Response.Redirect("~/Cobros/ConsultarCobros");
                    }
                    else
                    {
                        Response.Redirect("~/Cobros/CrearCobros");
                    }
                }
            }
            catch
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModels sesion)
        {

            //Tomamos la información de los textBox
            
            //Colocamos try en caso de error
            try
            {
                
                    //Realizamos la conexión con la BD
                    using (var db = new PviProyectoFinalDB("MyDatabase"))
                    {
                        //Utilizamos el StoreProcedure para validar la informacion del usuario y tomamos el primer resultado por defecto
                        var log = db.SpLogin(sesion.email, sesion.password).FirstOrDefault();//Utilizamos FirstOrDefault para que la BD entienda
                                                                            //que deseamos traer la única fila que retorna
                                                                            //Validamos que no venga nulo log
                        if (log != null)
                        {
                            //Validamos en caso de que la cuenta sea inactiva "I"
                            if (log.Estado == true)
                            {
                            //Creamos una instancia de usuario para utilizar sus atributos
                            LoginModels usuario = new LoginModels
                            {
                                id = log.IdPersona,
                                nombreCompleto = log.Nombre +" "+ log.Apellido,
                                esEmpleado = log.TipoPersona
                            };
                                //Realizamos una comprovación de si es o no empleado el usuario logeado
                                if (log.TipoPersona == "Empleado")
                                {//Asignamos el Estado empleado true para las paginas que le pertenecen al empleado y poder cambiarlo en 
                                 //las paginas que necesiten colocarlo como un cliente
                                    usuario.Estado = true;
                                    Session["Usuario"] = usuario;//Asignamos la sesion usuario con sus atributos
                                    Session["Nombreusuario"] = usuario.nombreCompleto;
                                    Response.Redirect("~/Cobros/ConsultarCobros", false);

                                }
                                else
                                {
                                    Session["Usuario"] = usuario;
                                    Session["Nombreusuario"] = usuario.nombreCompleto;
                                    Response.Redirect($"~/Cobros/ConsultarCobros", false);
                                }
                            }
                        }
                        else
                        {
                            Response.Write("Credenciales incorrectas o el usuario se encuentra inactivo");
                        }
                    }
            }
            catch
            {
                Response.Redirect($"~/Pages/Errores.aspx");
            }

            return View();

        }
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.RemoveAll(); // Elimina todos los datos de sesión
            return RedirectToAction("Login", "Login"); // Redirige al login u otra página   
        }
    }
}