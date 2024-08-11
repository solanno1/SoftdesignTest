﻿namespace LibrarySystem.Services.Interfaces
{
    public interface IUserService
    {
        bool ValidateUser(string username, string password);
        void RegisterUser(string username, string password);
    }
}
