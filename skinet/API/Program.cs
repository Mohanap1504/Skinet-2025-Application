using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();  
builder.Services.AddDbContext<Storedata>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

builder.Services.AddScoped<IRepositoryProduct, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
     .WithOrigins("http://localhost:4200","https://localhost:4200"));

app.MapControllers();

// try
// {
//   using var scope = app.Services.CreateScope();
//   var services = scope.ServiceProvider;
//   var context = services.GetRequiredService<Storedata>();
//   await context.Database.MigrateAsync();
//   await StoreContextSeed.SeedAsync(context);
// }
// catch (Exception ex)
// {
//   Console.WriteLine(ex);
//   throw;
// }

app.Run();
