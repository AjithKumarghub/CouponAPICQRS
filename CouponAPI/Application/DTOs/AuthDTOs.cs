namespace CouponAPI.Application.DTOs;

public record LoginRequestDTO(string UserName, string Password);
public record RegisterationRequestDTO(string UserName, string Name, string Password);
public record UserDTO(int Id, string UserName, string Name);
public record LoginResponseDTO(UserDTO User, string Token);
