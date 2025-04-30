using System;

namespace ECommerce.Application.DTOs.Favorite;

public sealed record FavoriteDto(
    int Id,
    int ProductId,
    string UserId,
    DateTime AddedDate
);