using System.Runtime.CompilerServices;
using ThiTracNghiemTrucTuyen.Api.Services;
using ThiTracNghiemTrucTuyen.Shared.DTOs;

namespace ThiTracNghiemTrucTuyen.Api.Endpoints
{
  public static class AuthEndpoints
  {
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
      app.MapPost("/api/auth/login", async (LoginDto loginDto, AuthService authService) => 
        Results.Ok(await authService.LoginAsync(loginDto)));
      return app;
    }
  }
}
