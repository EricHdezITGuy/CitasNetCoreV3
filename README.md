# Proyecto Agendador de Citas Online

Este proyecto está basado en ASP.NET Core y Entity Framework Core, con una base de datos MySQL.

## Pasos para configurar la aplicación

1. **Crear el Modelo de Datos:**
   Crea una carpeta 'Models' en tu proyecto y agrega una clase por cada tabla en tu base de datos. Por ejemplo, para la tabla 'Usuario', puedes crear una clase 'Usuario':

    ```csharp
    namespace AgendadorCitasOnline.Models
    {
        public class Usuario
        {
            public long Cedula { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string CorreoElectronico { get; set; }
        }
    }
    ```

2. **Crear una clase DbContext:**
   Esta clase representa una sesión con la base de datos, permitiéndonos consultar y guardar instancias de las entidades. En la carpeta 'Data', crea una clase 'ApplicationDbContext':

    ```csharp
    using Microsoft.EntityFrameworkCore;
    using AgendadorCitasOnline.Models;

    namespace AgendadorCitasOnline.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Usuario> Usuarios { get; set; }
            // Agrega aquí otras propiedades DbSet para las otras entidades.
        }
    }
    ```

3. **Configurar la cadena de conexión:**
   En tu archivo `appsettings.json`, agrega la cadena de conexión a tu base de datos MySQL dentro de la sección `ConnectionStrings`:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "server=localhost;port=3307;database=unleashedcitas;user=unleashed;password=unleashed"
    }
    ```

   Asegúrate de que la cadena de conexión es correcta para tu entorno.

4. **Registrar el DbContext:**
   En tu archivo `Program.cs`, en la configuración de los servicios, agrega la siguiente línea:

    ```csharp
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 11))));
    ```

   Recuerda reemplazar la versión de MySQL con la versión que estás usando.

5. **Configurar nombres de tablas en singular:**
   Debido a que EF Core convierte los nombres de las clases DbSet a plural, necesitamos configurar EF Core para que utilice los nombres de tabla en singular. Esto se hace en la clase 'ApplicationDbContext' así:

    ```csharp
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.GetDisplayName());
        }

        base.OnModelCreating(modelBuilder);
    }
    ```

6. **Crear una vista para verificar la funcionalidad:**
   Para comprobar que la conexión a la base de datos funciona correctamente, crea una vista que muestre la lista de usuarios. Primero, crea una carpeta 'Controllers' si no la tienes, y agrega una clase 'TestController':

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using AgendadorCitasOnline.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    namespace AgendadorCitasOnline.Controllers
    {
        public class TestController : Controller
        {
            private readonly ApplicationDbContext _context;

            public TestController(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                return View(usuarios);
            }
        }
    }
    ```

   Luego, crea una carpeta 'Test' dentro de la carpeta 'Views', y agrega un archivo `Index.cshtml` con el siguiente contenido:

    ```html
    @model List<AgendadorCitasOnline.Models.Usuario>

    <h1>Lista de Usuarios</h1>

    <ul>
        @foreach (var usuario in Model)
        {
            <li>@usuario.Nombre</li>
        }
    </ul>
    ```

   Finalmente, agrega la ruta para esta vista en tu archivo `Program.cs`:

    ```csharp
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Test}/{action=Index}/{id?}");
    ```

7. **Ejecutar el proyecto:**
   Luego de seguir estos pasos, puedes ejecutar tu proyecto y deberías ver la lista de usuarios al acceder a `localhost:5001/Test`.

## Problemas conocidos y soluciones

1. **No se puede encontrar la tabla 'Usuarios':**
   EF Core convierte los nombres de las clases DbSet a plural por defecto. Para solucionar esto, agregamos un bloque de código en el método `OnModelCreating()` de la clase `ApplicationDbContext` para forzar a EF Core a usar los nombres de tabla en singular.

2. **Mala configuración de la cadena de conexión:**
   Asegúrate de que la cadena de conexión en tu archivo `appsettings.json` es correcta para tu entorno.

## Próximos pasos

- Implementar la autenticación y autorización.
- Crear las vistas para las demás entidades.
- Agregar validación de datos en los formularios.
- Implementar funcionalidades adicionales según se necesite.

## Contribuciones

Este proyecto es un trabajo en progreso y cualquier contribución es bienvenida.

## Licencia

Este proyecto es de código abierto y está licenciado bajo los términos de la Licencia MIT.
