using Clinica.AccesoADatos;
using Clinica.AccesoADatos.JsonSettings;
using Clinica.Common;
using Clinica.Repositories.Implementacion;
using Clinica.Repositories.Implementacion.AdoRepository;
using Clinica.Repositories.Interfaces;
using Clinica.Repositories.Interfaces.IAdoRepository;
using Clinica.Services.Implementacion;
using Clinica.Services.Implementacion.GenerarPDF;
using Clinica.Services.Interfaces;
using Clinica.Services.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Implementación de serilog
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders();// quita el logger predeterminado
builder.Logging.AddSerilog(logger);

//vamos a leer el archivo de configuracion con una clase mapeada
builder.Services.Configure<AppConfiguration>(builder.Configuration);


// Add services to the container.


//Configuracion sql
var connectionString = builder.Configuration.GetConnectionString("ClinicaDB");
builder.Services.AddDbContext<ClinicaDbContext>(options => options.UseSqlServer(connectionString));

var connectionStringUsersDB = builder.Configuration.GetConnectionString("ApliccationDb");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStringUsersDB));

// Json setting model para la implementacion de ADO.Net
builder.Services.Configure<ConnectionSetting>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddIdentity<ClinicaIdentityUser, IdentityRole>(policies =>
{
    //politicas de  contraseña
    policies.Password.RequireDigit = true;
    policies.Password.RequireLowercase = true;
    policies.Password.RequireUppercase = false;
    policies.Password.RequireNonAlphanumeric = false;
    policies.Password.RequiredLength = 6;

    // todos los usuarios deben ser unicos
    policies.User.RequireUniqueEmail = true;

    //politica de bloqueo de cuentas
    policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    policies.Lockout.MaxFailedAccessAttempts = 5;


}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// AutoMapper
builder.Services.AddAutoMapper(config =>
config.AddProfile<Profiles>());

// inyección de dependencias

// services
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IMedicoServices, MedicoServices>();
builder.Services.AddTransient<IPacienteServices, PacienteServices>();
builder.Services.AddTransient<IConsultaServicescs, ConsultaServices>();
builder.Services.AddTransient<IEspecialidadServices, EspecialidadServices>();
builder.Services.AddTransient<IRecetarioServices, RecetarioServices>();
// Ado Repository
builder.Services.AddTransient<IAdoMedicoRepository,AdoMedicoRepository>();
builder.Services.AddTransient<IRecetarioAdoRepocsitory, RecetarioAdoRepository>();



//repositorios
builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();
builder.Services.AddTransient<IPacienteRepository, PacienteRepository>();
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
builder.Services.AddTransient<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddTransient<IRecetarioRepository, RecetarioRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion para el uso de la autenticación
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey") ??
        throw new InvalidOperationException("No se configuro el JWT"));

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Emisor"],
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//valida la autenticación (usuario y password)
app.UseAuthentication();
// validacion de permisios
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

    app.Run();
