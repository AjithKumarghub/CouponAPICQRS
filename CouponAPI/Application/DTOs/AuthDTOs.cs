namespace CouponAPI.Application.DTOs;

public record LoginRequestDTO(string Email, string Password);
public record RegisterationRequestDTO(string Email, string Name, string Password);
public record UserDTO(int Id, string Email, string Name);
public record LoginResponseDTO(UserDTO User, string Token);
