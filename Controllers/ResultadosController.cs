using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVI_Final_Condominios.Controllers
{
    public class ResultadosController : Controller
    {
        // GET: Resultados
        public ActionResult Index(string source)
        {
            //validamos sesion del usuario
            //if (Session["Usuario"] == null)
            //{
            //    Response.Redirect("~/Pages/Login.aspx");
            //}
            //Tomamos el source para mostrar diversos mensajes de resultados BUENOS
                switch (source)
                {
                    case "CrearReservacion":
                        ViewBag.Text = "La reservación ha sido registrada exitosamente en el sistema";
                        break;
                    case "ModificarReservacion":
                    ViewBag.Text = "Ha modificado correctamente la reservación";
                        break;
                    case "CancelarReservacion":
                    ViewBag.Text = "La reservación ha sido cancelada con éxito";
                        break;
                    case "CrearHabitacion":
                    ViewBag.Text = "Ha creado correctamente la habitación";
                        break;
                    case "EditarHabitacion":
                    ViewBag.Text = "Se ha logrado modificar de forma correcta la habitación";
                        break;
                    case "Inactivarhabitacion":
                    ViewBag.Text = "Se ha inactivado la habitación con éxito";
                        break;
                }
            //Tomamos el source para mostrar diversos mensajes de resultados MALOS
            switch (source)
            {
                case "CrearCasaM":
                    ViewBag.Text2 = "La casa que busca no existe";
                    ViewBag.Link = "/GestionarCasas/ConsultarCasas";
                    break;
                case "ModificarReservacion":
                    ViewBag.Text2 = "Ha modificado correctamente la reservación";
                    break;
                case "CancelarReservacion":
                    ViewBag.Text2 = "La reservación ha sido cancelada con éxito";
                    break;
                case "CrearHabitacion":
                    ViewBag.Text2 = "Ha creado correctamente la habitación";
                    break;
                case "EditarHabitacion":
                    ViewBag.Text2 = "Se ha logrado modificar de forma correcta la habitación";
                    break;
                case "Inactivarhabitacion":
                    ViewBag.Text2 = "Se ha inactivado la habitación con éxito";
                    break;
            }
            return View();
        }


    }
}