// Punto de entrada de la aplicacion (top-level statements).

using CafeteriaDinamica.Services;

var builder = WebApplication.CreateBuilder(args);

// Habilita el patron MVC (Controllers + Views con Razor).
builder.Services.AddControllersWithViews();

// La cola vive en memoria mientras corre la app, por eso el repositorio
// se registra como Singleton (una sola instancia compartida).
builder.Services.AddSingleton<RepositorioPedidos>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Pedidos/Index");
}

app.UseStaticFiles();

app.UseRouting();

// Ruta por defecto: /Controlador/Accion/id. Sin nada en la URL cae en Pedidos/Index.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pedidos}/{action=Index}/{id?}");

app.Run();
