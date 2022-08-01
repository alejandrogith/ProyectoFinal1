using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProyectoFinal.InicializarData;

namespace ProyectoFinal.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : Usuario
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que authenticationType debe coincidir con el valor definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar reclamaciones de usuario personalizadas aquí
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {





        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: true)


        { }

        public  DbSet<TipoDeArticulo> TipoDeArticulos { get; set; }
        public  DbSet<Categoria> Categoria { get; set; }

        public  DbSet<CondicionArticulo> CondicionArticulo { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

       public DbSet<Articulo> Articulo { get; set; }

        public DbSet<RecepcionEncabezado> RecepcionEncabezado { get; set; }

        public DbSet<RecepcionDetalle> RecepcionDetalle { get; set; }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}