using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _Entidades;
using _Helpers;
using _Entidades.specificModels;

namespace TicketsMVC.Controllers
{
    public class AuthenticateController : Controller
    {
        usuarioCRUD objUsuario = new usuarioCRUD();
       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email,string pwd)
        {
            try {
                if (objUsuario._Acceder(email, pwd))
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                ViewBag.errores = "Usuario o contraseña incorrectos";                
            }
            catch
            {
                ViewBag.errores = "ERROR: en el proceso de verificación";
            }
            return View();
        }

        public ActionResult Logout()
        {
            try {
                _SessionHelper.DestroyUserSession();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch
            {
                ViewBag.errores = "ERROR: No se pudo cerrar sesión de forma segura";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
