﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ThiTracNghiemTrucTuyen.Api.Data;
using ThiTracNghiemTrucTuyen.Api.Data.Entities;
using ThiTracNghiemTrucTuyen.Shared.DTOs;

namespace ThiTracNghiemTrucTuyen.Api.Services
{
  public class AuthService
  {
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
      _context = context;
      _passwordHasher = passwordHasher;
      _configuration = configuration;
    }

    public async Task LoginAsync(LoginDto loginDto)
    {
      var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == loginDto.Username);

      if (user == null)
      {
        // không có user này
        return;
      }
      var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

      if (passwordVerification == PasswordVerificationResult.Failed)
      {
        // sai mật khẩu
        return;
      }

      var jwt = GenerateJwtToken(user);

    }

    private string GenerateJwtToken(User user)
    {
      Claim[] claims =
        [
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Name),
          new Claim(ClaimTypes.Role, user.Role)
        ];

      var secretKey = _configuration.GetValue<string>("Jwt:Secret"); // lấy từ appsettings.json
      var symmetricKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
      var signingCred = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
      var jwtSecurityToken = new JwtSecurityToken(
            issuer: "your-issuer",                     // Định danh của người phát hành (Issuer)
            audience: "your-audience",                 // Định danh của đối tượng nhận token (Audience)
            claims: claims,                            // Các thông tin bổ sung (Claims)
            notBefore: DateTime.UtcNow,                // Thời điểm token bắt đầu có hiệu lực
            expires: DateTime.UtcNow.AddHours(1),      // Thời điểm token hết hạn
            //expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: signingCred
          );

      var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

      return token;
    }
  }
}
