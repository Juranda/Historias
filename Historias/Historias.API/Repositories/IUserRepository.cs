﻿namespace Historias.API.Repositories
{
    public interface IUserRepository
    {
        Task<string?> Login(string email, string password);
    }
}