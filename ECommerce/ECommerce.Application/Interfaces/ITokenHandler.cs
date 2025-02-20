using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces;

public interface ITokenHandler
{
    string GenerateAccessToken(ApplicationUser user, IList<string> roles);
    string GenerateRefreshToken();
}
