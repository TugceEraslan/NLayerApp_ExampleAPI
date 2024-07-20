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

/*Interfacelerin implementasyonalar�n�n da migrationa eklenmesi i�in a�a��daki i�lemleri yap�yoruz.*/
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>(); // IUnitOfWork ile kar��la��rsan UnitOfWork'u nesne �rne�i alacaks�n.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));  // Generic oldu�u i�in typeof olarak tan�ml�yoruz ve <> tan�ml�yoruz.
                                                                                        // IGenericRepository<> ile kar��la��rsan GenericRepository<>'u nesne �rne�i alacaks�n

//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        /* AppDbContext'i i�eren katman�n ad�n� al diyoruz.
         * Yani Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name diyerek asl�nda NLayer.Repository'i al�yoruz */
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);  // AppDbContext'i i�eren Assembly'nin ad�n� al diyoruz
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
