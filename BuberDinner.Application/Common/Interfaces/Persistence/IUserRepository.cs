﻿using BuberDinner.Domain.User;

namespace BuberDinner.Application.Common.Interfaces.Persistence;


public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}
