namespace WebApiPais.Models
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the <see cref="AplicationDbContext" />
    /// </summary>
    public class AplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //cuando se hereda de IdentityDbContext<ApplicationUser>, se crean las tablas para 
        //el manejo de usuarios.
        /// <summary>
        /// Initializes a new instance of the <see cref="AplicationDbContext"/> class.
        /// </summary>
        /// <param name="opcion">The opcion<see cref="DbContextOptions{AplicationDbContext}"/></param>
        public AplicationDbContext(DbContextOptions<AplicationDbContext> opcion) : base(opcion)
        {
        }

        /// <summary>
        /// Gets or sets the Paises
        /// </summary>
        public DbSet<Pais> Paises { set; get; }

        /// <summary>
        /// Gets or sets the Provincias
        /// </summary>
        public DbSet<Provincia> Provincias { set; get; }
    }
}
