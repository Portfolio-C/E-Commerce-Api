using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class RefreshToken : EntityBase
{
    public required string Token { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public bool IsRevoked { get; set; }

    public required string UserId { get; set; }
    public required ApplicationUser User { get; set; }
}
