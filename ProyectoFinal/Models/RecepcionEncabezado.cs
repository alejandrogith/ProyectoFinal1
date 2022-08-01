using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class RecepcionEncabezado
    {
        public int Id { get; set; }

        [NotMapped]
        public int Volumen { get; set; }
        [NotMapped]
        public int Numero { get; set; }
        [NotMapped]
        public int Año { get; set; }
        [NotMapped]
        public string Titulo { get; set; }

        public int Evaluciones { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name ="Fecha de recepcion")]
        public DateTime FechaRecepcion { get; set; }

       

        [ForeignKey("Articulo")]
        public int Articulo_Id { get; set; }
        public Articulo Articulo { get; set; }


        public int TipoDeArticulo_id { get; set; }
        public int Categoria_id { get; set; }



        public int Estado { get; set; }
    }
}