using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Usuario : IdentityUser
    {



        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string GradoAcademico { get; set; }
        public string Afiliacion { get; set; }
        public string Ocupacion { get; set; }
        public string InstitucionDondeTrabaja { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string Direccion { get; set; }
        public string AreaDeEspecializacion { get; set; }

        [NotMapped]
        public string NombreRol { get; set; }


        
        [ForeignKey("TipoDeArticulo")]
        public int TipoDeArticulo_Id { get; set; }
        public TipoDeArticulo TipoDeArticulo { get; set; }
        


        
        [ForeignKey("Categoria")]
        public int Categoria_Id { get; set; }
        public Categoria Categoria { get; set; }
        

    }
}