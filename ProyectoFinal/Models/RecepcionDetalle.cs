using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class RecepcionDetalle
    {

        public int Id { get; set; }

        [Display(Name ="Nombre del evaluador")]
        [NotMapped]
        public string NombreEvaluador { get; set; }

        [NotMapped]
        public string Titulo { get; set; }
        [NotMapped]
        public string Contenido { get; set; }

        public string Observacion { get; set; }

        [ForeignKey("Usuario")]
        public string Evaluador_Id { get; set; }
        public Usuario Usuario { get; set; }


        [ForeignKey("RecepcionEncabezado")]
        public int RecepcionEncabezado_Id { get; set; }
        public RecepcionEncabezado RecepcionEncabezado { get; set; }
    }
}