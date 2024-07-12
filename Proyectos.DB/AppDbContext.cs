using Microsoft.EntityFrameworkCore;


namespace Proyectos.DB

{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Miembros> Miembros { get; set; }
        public DbSet <Archivos> Archivos { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Referencias> Referencias { get; set; }
        public DbSet<Tags> Tags { get; set; }   
        public DbSet<Tags_Proyecto> Tags_Proyecto { get; set; }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)


        {
            // Configuración para establecer la fecha y hora actual por defecto
            modelBuilder.Entity<Proyectos>()
                .Property(p => p.Creacion)
                .HasDefaultValueSql("GETDATE()"); // Utiliza la función SQL específica de tu proveedor de base de datos (ejemplo: SQL Server)

            modelBuilder.Entity<Archivos>()
                .Property(p => p.Creacion)
                .HasDefaultValueSql("GETDATE()"); // Utiliza la función SQL específica de tu proveedor de base de datos (ejemplo: SQL Server)

            modelBuilder.Entity<Usuarios>()
                .Property(p => p.Creacion)
                .HasDefaultValueSql("GETDATE()"); // Utiliza la función SQL específica de tu proveedor de base de datos (ejemplo: SQL Server)

            modelBuilder.Entity<Comentarios>()
                .Property(p => p.Creacion)
                .HasDefaultValueSql("GETDATE()"); // Utiliza la función SQL específica de tu proveedor de base de datos (ejemplo: SQL Server)

            base.OnModelCreating(modelBuilder);
        }
    }
}
