using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*Interfacelerin implementasyonalarýnýn da migrationa eklenmesi için aþaðýdaki iþlemleri yapýyoruz.*/
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>(); // IUnitOfWork ile karþýlaþýrsan UnitOfWork'u nesne örneði alacaksýn.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  // Generic olduðu için typeof olarak tanýmlýyoruz ve <> tanýmlýyoruz.
                                                                                        // IGenericRepository<> ile karþýlaþýrsan GenericRepository<>'u nesne örneði alacaksýn

//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        /* AppDbContext'i içeren katmanýn adýný al diyoruz.
         * Yani Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name diyerek aslýnda NLayer.Repository'i alýyoruz */
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);  // AppDbContext'i içeren Assembly'nin adýný al diyoruz
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
