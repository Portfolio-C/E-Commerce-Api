using System;

namespace ECommerce.Application.Requests.Favorite;

public sealed record CreateFavoriteRequest(
    string UserId,
    int productId
);