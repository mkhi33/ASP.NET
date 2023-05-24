
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
    	"DefaultConnection": "Server=localhost;Database=Proyecto3MVCTareas;User=sa;Password=TU_PASSWORD;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True"
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

#### Utilizar Anotaciones de datos para personalizar los campos (DataAnnotations)

       [StringLength(250)]
       [Required]
       public string Titulo { get; set; }

#### Utilizar API fluente para personalizar los campos de la Entidad
En el ApplicationDbContext:

		override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tarea>().Property(t => t.Titulo).HasMaxLength(50).IsRequired();
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

# Configurar Identity con Entity Framework Core (ef core):

## instalar el paquete Identity para EF core:

	dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.0-preview.4.23260.4

## Modificar el ApplicationDbContext
Cambiar:

	public class ApplicationDbContext : DbContext

a: 

	public class ApplicationDbContext : IdentityDbContext


## Crear la migración

	dotnet ef migrations add Usuarios

	dotnet ef database update

## Configurar servicios para la implementación de Identity

	builder.Services.AddAuthentication();
	builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
	{
		opciones.SignIn.RequireConfirmedAccount = false;

	}).AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

## Configurar servicios para trabajar con vistas propias para el sistema de Usuarios

### Activar el sistema de autenticación por cookies	
	builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
	{
		opciones.LoginPath = "/Usuarios/Login";
		opciones.AccessDeniedPath = "/Usuarios/Login";
	}
	);

## Obtener la data del usuario autenticado

antes de app.useAuthorization()
	
	app.UseAuthentication();

## Crear una política para usuarios autenticados:

	var politicasUsuarioAutenticados = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();

	builder.Services.AddControllersWithViews(opciones =>
	{
		opciones.Filters.Add(new AuthorizeFilter(politicasUsuarioAutenticados));
	});

## Registro de usuarios

 		[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var usuario = new IdentityUser
            {
                Email = modelo.Email,
                UserName = modelo.Email
            };
            var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);
            if (resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(modelo);
            }
        }

## Autenticación de usuarios

    	[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }
            var resultado = await signInManager.PasswordSignInAsync(modelo.Email, modelo.Password, modelo.Recuerdame, lockoutOnFailure: false);
            if(resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña no válido");
                return View(modelo);
            }
        }

## Cerrar la sesión del usuario

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Transacciones");
        }

# Internacionalización

    Es el proceso de tener una aplicación que funcione para distintas culturas.
Se divide en dos partes:
- Globalización
- Localozación

## Globalización:

    Es el proceso de diseñar una aplicación para que funcione con distintas culturas.
## Localización:

    Es el proceso de tomar una aplicación globalizada y aplicarle una cultura especifica.

### Configurar localización en ASP.NET

En la clase Program.cs

    builder.Services.AddLocalization();

Implementanto Localización en homeController:

- Inyección de dependencia IStringLocalizer:

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            this.localizer = localizer;
        }

- Utilizando localizer (Ejemplo utilizando el ViewBag):

        public IActionResult Index()
        {
            ViewBag.Saludo = localizer["Buenos días"];
            return View();
        }

## Configurar Program.cs para soportar diferentes culturas:

    var culturasUISoportadas = new[] { "es", "en" };
    app.UseRequestLocalization(opciones =>
    {
        opciones.DefaultRequestCulture = new RequestCulture("es");
        opciones.SupportedCultures = culturasUISoportadas.Select(cultura => new CultureInfo(cultura)).ToList();
    });

## Configurar la carpeta de recursos

- Crear la carpeta Recursos en la raíz.

        builder.Services.AddLocalization(opciones =>
        {
            opciones.ResourcesPath = "Recursos";
        });

## Implementar ViewLocalization

        // Add services to the container.
        builder.Services.AddControllersWithViews(opciones => 
        {
            opciones.Filters.Add(new AuthorizeFilter(politicasUsuarioAutenticados));
        }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

