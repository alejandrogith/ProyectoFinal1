using Newtonsoft.Json;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{

    public class ArticulosPublicados{

        public int Volumen { get; set; }
        public int Numero { get; set; }
        public string Año { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public Guid DOI { get; set; }
        public string TipoDeArticulo { get; set; }
        public string Categoriadearticulo { get; set; }
        public string Autor { get; set; }
        
        public string FechaPublicacion { get; set; }
    }

    public class ArticulosPublicadosController : Controller
    {
        // GET: ArticulosPublicados
        public  ActionResult Index()
        {
            var AppDB = new ApplicationDbContext();

            var Articulos = AppDB.Articulo.Where(A=>A.Estado=="Aprobado").ToList();

            var ArticulosPub = new List<ArticulosPublicados>();

            foreach (var Articulo in Articulos) {
                var ArticuloPub = new ArticulosPublicados();

                ArticuloPub.Titulo = Articulo.Titulo;
                ArticuloPub.Contenido = Articulo.Contenido;
                ArticuloPub.DOI = Articulo.DOI;

                ArticuloPub.Volumen=Articulo.Volumen;
                ArticuloPub.Numero = Articulo.Numero;
                ArticuloPub.Año = Articulo.Año.Year.ToString();

                var user = AppDB.Users.Find(Articulo.Usuario_Id);
                ArticuloPub.Autor = user.Nombre;

                var categoria = AppDB.Categoria.Find(Articulo.Categoria_Id);
                ArticuloPub.Categoriadearticulo = categoria.Descripcion;

                var tipo = AppDB.TipoDeArticulos.Find(Articulo.TipoDeArticulo_Id);
                ArticuloPub.TipoDeArticulo = tipo.Descripcion;


                DateTime date = DateTime.Now;
                ArticuloPub.FechaPublicacion = date.ToString("dd-MM-yyyy");


                ArticulosPub.Add(ArticuloPub);
            }



            return Json(ArticulosPub, JsonRequestBehavior.AllowGet);
        }



    }
}