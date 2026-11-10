using Veterinaria.Negocios;

var builder = WebApplication.CreateBuilder(args);

// Buscar la cadena de conexión de forma segura
string? cadena = builder.Configuration.GetConnectionString("Veterinaria");

if (!string.IsNullOrWhiteSpace(cadena))
{
    ConfiguracionNegocio.Inicializar(cadena);
}
else
{
    System.Diagnostics.Debug.WriteLine(
        "AVISO: No se encontró ConnectionStrings:Veterinaria. Revise User Secrets.");
}

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