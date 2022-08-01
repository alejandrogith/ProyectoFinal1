using Microsoft.Owin;
using Owin;
using ProyectoFinal.InicializarData;
using ProyectoFinal.Models;
using System.Data.Entity;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(ProyectoFinal.Startup))]
namespace ProyectoFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {


            ConfigureAuth(app);
        }
    }
}
