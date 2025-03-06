using Core.Models;

namespace Application.DTO;

public record TokensDTO(
    string AccessToken,
    RefreshToken RefreshToken);