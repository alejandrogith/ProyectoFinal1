using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class TipoDeArticulo
    {

        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}