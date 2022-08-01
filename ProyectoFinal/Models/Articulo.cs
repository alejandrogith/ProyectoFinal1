using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Articulo
    {
        public int Id { get; set; }
        public int Volumen { get; set; }
        public int Numero { get; set; }
        
        [Required]
        public DateTime Año { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public Guid DOI { get; set; }




        [JsonIgnore]
        [ForeignKey("Usuario")]
        public string Usuario_Id { get; set; }
        public Usuario Usuario { get; set; }


        [JsonIgnore]
        [ForeignKey("TipoDeArticulo")]
        [Required]
        public int TipoDeArticulo_Id { get; set; }
        public TipoDeArticulo TipoDeArticulo { get; set; }

        [JsonIgnore]
        [ForeignKey("Categoria")]
        public int Categoria_Id { get; set; }
        public Categoria Categoria { get; set; }


        [NotMapped]
        [Display(Name = "Tipo De Articulo")]
        public string NombreTipoDeArticulo { get; set; }


        [NotMapped]
        [Display(Name = "Categoria de articulo")]
        public string NombreCategoriadearticulo { get; set; }

        [NotMapped]
        public string NombreAutor { get; set; }

        public string Estado { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime?  FechaDeAprobacion { get; set; }

        [JsonIgnore]
        public int NotificacionEnc { get; set; }
    }
}