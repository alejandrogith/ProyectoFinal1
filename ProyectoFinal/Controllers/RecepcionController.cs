using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class RecepcionController : Controller
    {
 
        public ActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var appDB = new ApplicationDbContext();

            
            var usuario=appDB.Usuarios.First(x => x.Id == usuarioActual.Value);

            var recencionesencabezado = appDB.RecepcionEncabezado.ToList().Where(U => U.TipoDeArticulo_id == usuario.TipoDeArticulo_Id && U.Categoria_id==usuario.Categoria_Id);

            foreach (var recepcionE in recencionesencabezado) {




                var articulo = appDB.Articulo.Find(recepcionE.Articulo_Id);

                recepcionE.Volumen = articulo.Volumen;
                recepcionE.Numero = articulo.Numero;
                recepcionE.Año = articulo.Año.Year;
                recepcionE.Titulo = articulo.Titulo;


                var Nevaluacion = appDB.RecepcionDetalle.Where(E=>E.RecepcionEncabezado_Id==recepcionE.Id).Count();

                recepcionE.Evaluciones = Nevaluacion;

                recepcionE.FechaRecepcion = DateTime.Now;



                if (recepcionE.Estado==0)
                {
                    ViewData["Evaluado"]= "En proceso de evaluacion";
                }

                if (recepcionE.Estado == 1)
                {
                    ViewData["Evaluado"] = "Aprobado";
                }

                if (recepcionE.Estado == 2)
                {
                    ViewData["Evaluado"] = "Aprobado con observaciones";
                }

                if (recepcionE.Estado == 3)
                {
                    ViewData["Evaluado"] = "Rechazado";
                }



            }

            var recencionesencabezado2 = from U in recencionesencabezado select U;




            return View(recencionesencabezado2);
        }














        // GET: Recepcion/Evaluar
        public ActionResult Evaluar(int id)
        {
            var AppDB = new ApplicationDbContext();

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            




            var recepcion= AppDB.RecepcionEncabezado.Find(id);
            var evaluador = AppDB.Usuarios.Find(usuarioActual.Value);
            var articulo = AppDB.Articulo.First(A=>A.Id== recepcion.Articulo_Id);


            var recepdetvm = new RecepcionDetalleVM();
            var recep = new RecepcionDetalle();
            recep.Titulo= articulo.Titulo;
            recep.Contenido = articulo.Contenido;
            recep.Evaluador_Id = evaluador.Id;
            recep.RecepcionEncabezado_Id = id;
            recepdetvm.RecepcionDetalle = recep;



            var recepdetalls = AppDB.RecepcionDetalle.Where(x => x.RecepcionEncabezado_Id == id).ToList();
            recepdetvm.ListaRecepcionDetalles= recepdetalls;


            foreach (var recepd in recepdetvm.ListaRecepcionDetalles) {

                var user = AppDB.Usuarios.Find(recepd.Evaluador_Id);


                recepd.NombreEvaluador = user.Nombre;
            }

           

            return View(recepdetvm);
        }




        // POST: Recepcion/Evaluar
        [HttpPost]
        public ActionResult Evaluar(RecepcionDetalleVM recepvm)
        {

            var AppDB = new ApplicationDbContext();

            recepvm.RecepcionDetalle.Observacion = "";

            AppDB.RecepcionDetalle.Add(recepvm.RecepcionDetalle);
            AppDB.SaveChanges();





            return RedirectToAction("Index");
        }


        // POST: Recepcion/Comentario
        [HttpPost]
        public ActionResult Comentario(FormCollection formCollection)
        {
            String EvaluadorId = formCollection["Evaluador_Id"];
            String RecepcionEncId = formCollection["RecepcionEncabezado_Id"];
            String Observacion = formCollection["Observacion"];

            var AppDB = new ApplicationDbContext();

            var recepcion = AppDB.RecepcionDetalle.FirstOrDefault(R => R.Evaluador_Id == EvaluadorId);


            recepcion.Observacion = Observacion;
            recepcion.Evaluador_Id= EvaluadorId;
            recepcion.RecepcionEncabezado_Id = Int32.Parse(RecepcionEncId);

       

            AppDB.Entry(recepcion).State = System.Data.Entity.EntityState.Modified;
            AppDB.SaveChanges();




            return RedirectToAction(nameof(Evaluar), new { @id = Int32.Parse( RecepcionEncId) }); ;
        }



        [HttpGet]
        public ActionResult Evaluado(int cond,int enc) {

            var AppDB = new ApplicationDbContext();
            var resenc = AppDB.RecepcionEncabezado.Find(enc);
            

            resenc.Estado = cond;
            AppDB.Entry(resenc).State = System.Data.Entity.EntityState.Modified;
            AppDB.SaveChanges();


            if (cond==1) {
                var articulo = AppDB.Articulo.Find(resenc.Articulo_Id);
                articulo.Estado = "Aprobado";
                articulo.DOI = Guid.NewGuid();
                articulo.NotificacionEnc = 0;
                AppDB.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
                AppDB.SaveChanges();

            }

            if (cond == 2){

                var articulo = AppDB.Articulo.Find(resenc.Articulo_Id);
                articulo.Estado = "Aprobado con observaciones";
                articulo.NotificacionEnc = enc;
                AppDB.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
                AppDB.SaveChanges();

                
            }

            if (cond == 3){
                var articulo = AppDB.Articulo.Find(resenc.Articulo_Id);
                articulo.Estado = "Rechazado";
                AppDB.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
                AppDB.SaveChanges();
            }

            return RedirectToAction("Index"); ;
        }


  





        // POST: Recepcion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recepcion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recepcion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
