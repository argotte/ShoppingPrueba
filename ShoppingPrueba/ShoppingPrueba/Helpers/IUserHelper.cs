﻿using Microsoft.AspNetCore.Identity;
using ShoppingPrueba.Data.Entities;
using ShoppingPrueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPrueba.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);//es asincrono

        Task<IdentityResult> AddUserAsync(User user, string password);//Crea usuarios con su pw
        Task<User> AddUserAsync(AddUserViewModel model);//Crea usuarios con su pw

        Task CheckRoleAsync(string roleName);//Chequea si un rol no existe: Si no existe lo crea

        Task AddUserToRoleAsync(User user, string roleName);//asigna si un rol pertenece a usuarios o admins

        Task<bool> IsUserInRoleAsync(User user, string roleName);//Verifica con booleano si dice si es admin o user

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<User> GetUserAsync(Guid userId);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

    }
}
