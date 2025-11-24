using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pedidos.Entidades;
using Pedidos.Entidades.MicrosoftIdentity;
namespace Pedidos.Datos
{
    public class DbDataAcces : IdentityDbContext<User,Role,Guid,UserClaim,UserRole, UserLogin,  RoleClaim, UserToken>
    {
        /*Hereda de IdentityDbContext, que es una clase de Entity Framework Core
preparada para trabajar con usuarios, roles y autenticación (sistema Identity de ASP.NET).
👉 Esto quiere decir que además de tus tablas(Libros, Autores, etc.), 
este contexto también puede manejar tablas relacionadas con usuarios
(AspNetUsers, AspNetRoles, etc.), si usás login en tu sistema.*/

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<DetallePedido> DetallesPedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }


        public DbDataAcces(DbContextOptions<DbDataAcces> options):base(options)
        {
            /*DbContextOptions<DbDataAccess> indica que las opciones de configuración
            (como la cadena de conexión, el proveedor SQL, etc.) están asociadas a este contexto 
            en particular (DbDataAccess).

            Es decir: Entity Framework necesita saber qué contexto estás configurando 
            cuando creás la conexión.

            : base(options) pasa esas opciones al constructor de la clase base DbContext, 
            que es quien realmente usa esa configuración para conectarse a la base de datos.

             En palabras simples

             “Cuando crees un DbDataAccess, te voy a pasar las opciones que indican cómo conectarte 
            a la base de datos, y vos (la clase DbContext) usá esas opciones.”*/
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }



    }
}
