namespace CouponAPI.Application.DTOs;

public record CouponCreateDTO(string Name, int Percent, bool IsActive);
public record CouponUpdateDTO(int Id, string Name, int Percent, bool IsActive);
