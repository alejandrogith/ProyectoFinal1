using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.ViewModels
{
    public class UsuarioVM
    {
        public Usuario Usuario { get; set; }

        public IEnumerable<SelectListItem> ListaCategorias { get; set; }

        public IEnumerable<SelectListItem> ListaTiposDeArticulo { get; set; }

    }
}