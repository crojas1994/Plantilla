

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VentanillaUnica.Tramites.Api.Filters;
using VentanillaUnica.Tramites.Application.Extensions;
using VentanillaUnica.Tramites.Data;
using VentanillaUnica.Tramites.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//Configure Services
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)));

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "VentanillaUnica.Tramites.Api",
        Description = "Microservicio para la gestión de tramites"
    });

    setupAction.CustomSchemaIds(schema => schema.FullName);
});

builder.Services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("Tramites"));

// Add services to the container.
builder.Services.AddServiceDependency();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        setupAction.SwaggerEndpoint("/swagger/V1/swagger.json", "VentanillaUnica.Tramites.Api");
    });
}

app.UseCors("AllowAll");

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "master-only");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Cache-Control", "no-cache,no-store,must-revalidate");
    context.Response.Headers.Add("Pragma", "no-cache");
    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Remove("Server");
    await next();
});

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider.GetService<IUnitOfWork>().EnsureSeeded();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
