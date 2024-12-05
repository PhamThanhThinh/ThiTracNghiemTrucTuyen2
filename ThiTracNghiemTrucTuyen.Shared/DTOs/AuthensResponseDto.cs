using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThiTracNghiemTrucTuyen.Shared.DTOs
{
  // viết gọn
  public record AuthensResponseDto(string Token, string? ErrorMessage = null)
  {
    [JsonIgnore]
    public bool HasError => ErrorMessage != null;
  }

  // viết rõ ràng ra
  //public record AuthensResponseDto
  //{
  //  public string Token { get; init; }
  //  public string? ErrorMessage { get; init; }

  //  // Constructor chính với kiểm tra tham số rõ ràng
  //  public AuthensResponseDto(string token, string? errorMessage = null)
  //  {
  //    Token = token ?? throw new ArgumentNullException(nameof(token), "Token không được để trống.");
  //    ErrorMessage = errorMessage;
  //  }

  //  // Thuộc tính để kiểm tra lỗi
  //  public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
  //}

}
