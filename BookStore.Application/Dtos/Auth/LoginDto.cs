﻿namespace BookStore.Application.Dtos.Auth;

public class LoginDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
