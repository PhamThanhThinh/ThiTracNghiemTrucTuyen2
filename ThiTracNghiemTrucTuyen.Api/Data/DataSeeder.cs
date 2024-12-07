using Microsoft.AspNetCore.Identity;
using ThiTracNghiemTrucTuyen.Api.Data;
using ThiTracNghiemTrucTuyen.Api.Data.Entities;

public static class DataSeeder
{
  public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
  {
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();

    if (!context.Users.Any(u => u.Email == "admin@domainbatky.test.xyz"))
    {
      try
      {
        var adminUser = new User
        {
          Id = 4,
          Name = "admin",
          Email = "admin@domainbatky.test.xyz",
          Phone = "123456799",
          Role = nameof(UserRole.Admin),
        };

        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin12345!@#$%");

        context.Users.Add(adminUser);
        await context.SaveChangesAsync();
        Console.WriteLine("Tài khoản Admin đã được tạo thành công.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi khởi tạo tài khoản Admin: {ex.Message}");
      }
    }
    else
    {
      Console.WriteLine("Tài khoản Admin bị trùng, 'admin' đã được tạo trước đó");
    }
  }
}
