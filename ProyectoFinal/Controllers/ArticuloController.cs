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
    public class ArticuloController : Controller
    {
        // GET: Articulo
        public ActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var appDB = new ApplicationDbContext();


            var Articulos = from U in appDB.Articulo.ToList().Where(U => U.Usuario_Id == usuarioActual.Value) select U;


            foreach (var Articulo in Articulos) {


                var res = appDB.TipoDeArticulos.Find(Articulo.TipoDeArticulo_Id);

                var res2 = appDB.Categoria.Find(Articulo.Categoria_Id);


                Articulo.NombreTipoDeArticulo = res.Descripcion;
                Articulo.NombreCategoriadearticulo = res2.Descripcion;

                if (Articulo.Estado is null) {
                    Articulo.Estado = "En espera de evaluacion";
                }


            }

           




            return View(Articulos);
        }



        // GET: Articulo/Create
        public ActionResult Create()
        {

            var AppDB = new ApplicationDbContext();

            var articulovm = new ArticuloVM();


            articulovm.ListaTiposDeArticulo = AppDB.TipoDeArticulos.Select(i => new SelectListItem
            {
                Text = i.Descripcion,
                Value = i.Id.ToString()
            });

            articulovm.ListaCategoriaDeArticulo = AppDB.Categoria.Select(i => new SelectListItem
            {
                Text = i.Descripcion,
                Value = i.Id.ToString()
            });

            return View(articulovm);
        }

        // POST: Articulo/Create
        [HttpPost]
        public ActionResult Create(ArticuloVM articulovm)
        {
            if (ModelState.IsValid) {
    

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            articulovm.Articulo.Usuario_Id=usuarioActual.Value;



            var AppDB = new ApplicationDbContext();

            AppDB.Articulo.Add(articulovm.Articulo);


            AppDB.SaveChanges();


            }
            return RedirectToAction("Index");

        }

        // GET: Articulo/Edit/5
        public ActionResult Edit(int id)
        {
            var AppDB = new ApplicationDbContext();

            var Articulo = AppDB.Articulo.Find(id);

            var articulovm = new ArticuloVM();

            articulovm.Articulo=Articulo;

            articulovm.ListaTiposDeArticulo = AppDB.TipoDeArticulos.Select(i => new SelectListItem
            {
                Text = i.Descripcion,
                Value = i.Id.ToString()
            });


            articulovm.ListaCategoriaDeArticulo = AppDB.Categoria.Select(i => new SelectListItem
            {
                Text = i.Descripcion,
                Value = i.Id.ToString()
            });



            return View(articulovm);
        }

        // POST: Articulo/Edit/5
        [HttpPost]
        public ActionResult Edit(ArticuloVM articulovm)
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            articulovm.Articulo.Usuario_Id = usuarioActual.Value;

 

            var AppDB = new ApplicationDbContext();


         
           AppDB.Entry(articulovm.Articulo).State = System.Data.Entity.EntityState.Modified;

            AppDB.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: Articulo/Delete/5
        public ActionResult Delete(int id)
        {

            var AppDB = new ApplicationDbContext();

            var Articulo = AppDB.Articulo.Find(id);



            return View(Articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost]
        public ActionResult Delete(Articulo articulo)
        {

        
            var AppDB = new ApplicationDbContext();



            AppDB.Entry(articulo).State = System.Data.Entity.EntityState.Deleted;

            AppDB.SaveChanges();


            return RedirectToAction("Index");

        }



        // GET: Articulo/Delete/5
        public ActionResult Recepcion(int id)
        {

            var AppDB = new ApplicationDbContext();

            var articulo = AppDB.Articulo.Find(id);


            var recepcion = new RecepcionEncabezado();

            recepcion.Articulo_Id = articulo.Id;

            recepcion.FechaRecepcion=DateTime.Now;


            recepcion.TipoDeArticulo_id=articulo.TipoDeArticulo_Id;
            recepcion.Categoria_id=articulo.Categoria_Id;


            AppDB.RecepcionEncabezado.Add(recepcion);

            AppDB.SaveChanges();


            return RedirectToAction("Index");
        }






        // GET: Articulo/Delete/5
        public ActionResult Notificaciones() {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var UserID = usuarioActual.Value;

            var AppDB = new ApplicationDbContext();

            var articulos=from AR in  AppDB.Articulo.Where(A=>A.NotificacionEnc!=0&&A.Usuario_Id== UserID).ToList()  select AR;


            var res =new List<RecepcionEncabezado>();
            
            foreach (var articulo in articulos) {

                var notenc =  AppDB.RecepcionEncabezado.FirstOrDefault(R =>R.Id==articulo.NotificacionEnc) ;
                if (notenc != null) {
                    res.Add(notenc);
                }
                
            }



            return View(from R in res select R);
        }



        public ActionResult Observaciones(int id)
        {
            var AppDB = new ApplicationDbContext();


            var obserb = AppDB.RecepcionDetalle.Where(x => x.RecepcionEncabezado_Id == id).ToList();

            foreach (var recepd in obserb)
            {

                var user = AppDB.Usuarios.Find(recepd.Evaluador_Id);


                recepd.NombreEvaluador = user.Nombre;
            }


            var res = from R in obserb select R;
            return View(res);
        }





        }
}
