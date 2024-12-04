using ThiTracNghiemTrucTuyen.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ThiTracNghiemTrucTuyen.Api.Data.Entities;
using ThiTracNghiemTrucTuyen.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  var chuoiKetNoi = builder.Configuration.GetConnectionString("conn");
  options.UseSqlServer(chuoiKetNoi);
}
);

builder.Services.AddTransient<AuthService>();


var app = builder.Build();

#if DEBUG
ApplyDbMigration(app.Services);
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
  var forecast = Enumerable.Range(1, 5).Select(index =>
      new WeatherForecast
      (
          DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          Random.Shared.Next(-20, 55),
          summaries[Random.Shared.Next(summaries.Length)]
      ))
      .ToArray();
  return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();




app.Run();

//static void ApplyDbMigration(IServiceProvider serviceProvider)
//{
//  var scope = serviceProvider.CreateScope();
//  var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
//  if (context.Database.GetPendingMigrations().Any())
//  {
//    context.Database.Migrate();
//  }
//}
static void ApplyDbMigration(IServiceProvider serviceProvider)
{
  try
  {
    using (var scope = serviceProvider.CreateScope())
    {
      var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

      // Kiểm tra và áp dụng các migration còn thiếu
      if (context.Database.GetPendingMigrations().Any())
      {
        context.Database.Migrate();
        Console.WriteLine("Database migration applied successfully.");
      }
      else
      {
        Console.WriteLine("No pending migrations found.");
      }
    }
  }
  catch (Exception ex)
  {
    Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
    // Log lỗi nếu cần
  }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


//#if DEBUG
//ApplyDbMigration(app.Services);
//#endif

//static void ApplyDbMigration(IServiceProvider serviceProvider)
//{
//  try
//  {
//    using (var scope = serviceProvider.CreateScope())
//    {
//      var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//      // Kiểm tra và áp dụng các migration còn thiếu
//      if (context.Database.GetPendingMigrations().Any())
//      {
//        context.Database.Migrate();
//        Console.WriteLine("Database migration applied successfully.");
//      }
//      else
//      {
//        Console.WriteLine("No pending migrations found.");
//      }
//    }
//  }
//  catch (Exception ex)
//  {
//    Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
//    // Log lỗi nếu cần
//  }
//}
