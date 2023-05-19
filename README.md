
Curso de ASP.NET
# ASP.NET
En este repositorio se encuentran los proyectos realizados en el curso de ASP.NET

# Entity Framework Core

### Instalar EF CLI
		
		dotnet tool install --global dotnet-ef

### Instalar el connector de SQL Server

	dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0-preview.4.23259.3
### Para vscode

	dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0-preview.4.23259.3 

### Para Visual Studio
	dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0-preview.4.23259.3


## Configurar la conexión con SQL server
### Crear la cadena de conexión en:
 appsettings.Development.json

	"ConnectionStrings": {
    	"DefaultConnection": "Server=localhost;Database=Proyecto3MVCTareas;User=sa;Password=@Demon2017;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True"
  	},

### Crear una Entidad para representar una tabla en la Base de datos

	namespace TareasMVC.Entidades
	{
		public class Tarea
		{
		public int Id { get; set; } 
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public int Orden { get; set; }
		public DateTime FechaCreacion { get; set; }
		}
	}
### Crear el ApplicationDbContext.cs
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

		// DbSet le indica a Entity Framework Core que Tareas es una Entidad

        public DbSet<Tarea> Tareas { get; set; }
    }
### Configurar Program.cs para hacer disponible el servicio DBContext

	builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

### Crear una migracion desde ef cli

	dotnet ef migrations add Tareas

### Actualizar la base de datos a partir de la migración

	dotnet ef update database
