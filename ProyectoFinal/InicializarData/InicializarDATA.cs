using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProyectoFinal.Models;
using ProyectoFinal.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProyectoFinal.InicializarData
{



    public class InicializarDATA 
    {

        public void InicializarDatos()
            {


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));



            
            //Aqui validamos si los roles existen sino se crean
            if (!roleManager.RoleExists(RolUsuario.Admin))
               {
                roleManager.Create(new IdentityRole(  RolUsuario.Admin));
                roleManager.Create(new IdentityRole( RolUsuario.Autor));
                roleManager.Create(new IdentityRole( RolUsuario.Evaluador));
              }

            

            var context = new ApplicationDbContext();
            if (context.Categoria.ToList().Count() == 0)
            {
          
            

            List<Categoria> listaCategorias = new List<Categoria>();
            

            listaCategorias.Add(new Categoria() { Descripcion = "Innovación tecnológica" });
            listaCategorias.Add(new Categoria() { Descripcion = "Innovación pedagógica" });
            listaCategorias.Add(new Categoria() { Descripcion = "Innovación social" });
            listaCategorias.Add(new Categoria() { Descripcion = "Investigación con aportes innovadores" });

            context.Categoria.AddRange(listaCategorias);

            List<CondicionArticulo> listaCondicionArticulo = new List<CondicionArticulo>();

            listaCondicionArticulo.Add(new CondicionArticulo() { Descripcion = "Aprobado" });
            listaCondicionArticulo.Add(new CondicionArticulo() { Descripcion = "Aprobado con observaciones" });
            listaCondicionArticulo.Add(new CondicionArticulo() { Descripcion = "Rechazado" });


            context.CondicionArticulo.AddRange(listaCondicionArticulo);


            List<TipoDeArticulo> listaTiposArticulos = new List<TipoDeArticulo>();

            listaTiposArticulos.Add(new TipoDeArticulo() { Descripcion = "Artículo científico" });
            listaTiposArticulos.Add(new TipoDeArticulo() { Descripcion = "Estudio de caso" });
            listaTiposArticulos.Add(new TipoDeArticulo() { Descripcion = "Revisión bibliográfica" });
            listaTiposArticulos.Add(new TipoDeArticulo() { Descripcion = "Ensayo académico" });
            listaTiposArticulos.Add(new TipoDeArticulo() { Descripcion = "Recensión de libro" });
            listaTiposArticulos.Add(new TipoDeArticulo() { Descripcion = "Editorial" });
            listaTiposArticulos.Add(new TipoDeArticulo() { Descripcion = "Prólogo" });


            context.TipoDeArticulos.AddRange(listaTiposArticulos);


            context.SaveChanges();


            }
        }



    }

}