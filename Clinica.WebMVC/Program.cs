using Microsoft.AspNetCore.Authentication.Cookies;
using Clinica.WebMVC.Services.Implementaciones;
using Clinica.WebMVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Patron singelton para el objeto HttpClient



builder.Services.AddSingleton( _=> new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("Backend:ClinicaApiRest"))
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<IUserProxy, Userproxy>();
builder.Services.AddScoped<IUbigeoProxy, UBigeoProxy>();
builder.Services.AddScoped<IEspecialidadProxy, EspecialidadProxy>();
builder.Services.AddScoped<IConsultaProxy, ConsultaProxy>();

builder.Services.AddDistributedMemoryCache();

//creación de sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120); //pasado 2 horas las sesion caduca
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "ProyectoEmpresarial";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        options.LoginPath = "/UsersControllers/Login";
        options.AccessDeniedPath = "/User/AccesoDenegado";// sin acceso en la aplicación
        options.SlidingExpiration = true;
    
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
