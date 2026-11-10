using Veterinaria.Negocios;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión con MySQL
string cadenaConexion =
    builder.Configuration.GetConnectionString("Veterinaria")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión Veterinaria.");

ConfiguracionNegocio.Inicializar(cadenaConexion);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();