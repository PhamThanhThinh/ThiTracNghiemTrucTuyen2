using Refit;
using ThiTracNghiemTrucTuyen.Shared.DTOs;

namespace ThiTracNghiemTrucTuyen.Web.Services
{
  public interface IAuthApi
  {
    [Post("/api/auth/login")]
    Task<AuthensResponseDto> LoginAsync(LoginDto loginDto);
  }
}
