using System;

namespace ECommerce.Application.Requests.Favorite;

public sealed record UpdateFavoriteRequest(
    int Id,
    int ProductId,
    string UserID
);