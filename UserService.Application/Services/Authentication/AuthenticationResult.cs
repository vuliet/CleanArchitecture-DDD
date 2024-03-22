﻿using UserService.Domain.Entities;

namespace UserService.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token);
}
