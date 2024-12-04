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
                    case "CrearCobroB":
                        ViewBag.Text = "El cobro se ha guardado con éxito";
                    ViewBag.Link = "/Cobros/ConsultarCobros";
                    break;
                    case "ModificarCobroB":
                    ViewBag.Text = "El cobro se ha modificado con éxito";
                    ViewBag.Link = "/Cobros/ConsultarCobros";
                    break;
                    case "CrearCasaB":
                    ViewBag.Text = "La reservación ha sido cancelada con éxito";
                    ViewBag.Link = "/GestionarCasas/ConsultarCasas";
                    break;
                    case "ModificarCasaB":
                    ViewBag.Text = "Ha creado correctamente la habitación";
                    ViewBag.Link = "/GestionarCasas/ConsultarCasas";
                    break;
                }
            //Tomamos el source para mostrar diversos mensajes de resultados MALOS
            switch (source)
            {
                case "BuscarCasaM":
                    ViewBag.Text2 = "La casa que busca no existe";
                    ViewBag.Link = "/GestionarCasas/ConsultarCasas";
                    break;
                case "ModificarCasaM":
                    ViewBag.Text2 = "No es posible modificar esta casa ya que tiene cobros pendientes";
                    ViewBag.Link = "/GestionarCasas/ConsultarCasas";
                    break;
            }
            return View();
        }


    }
}