﻿using ECommerce.Domain.Enums;

namespace ECommerce.Application.Requests;

public sealed record RegisterRequest(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password,
    ApplicationUserRole Role);
