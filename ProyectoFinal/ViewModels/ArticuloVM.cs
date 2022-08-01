using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.ViewModels
{
    public class ArticuloVM
    {

        public Articulo Articulo { get; set; }



        public IEnumerable<SelectListItem> ListaTiposDeArticulo{ get; set; }
        public IEnumerable<SelectListItem> ListaCategoriaDeArticulo { get; set; }
    }
}