using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DTO.Users;
using Data.Entities;

namespace WebApi.Services.UserService
{


    public interface IUserService
    {
        AuthenticateResponse Authenticate(User user, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        IEnumerable<User> GetAll();
        User GetById(Guid id);

        User AddUser(User user);

        bool ActivateUser(Guid token);

        Guid? RequestChangePassword(string email);

        bool ChangePassword(Guid token, string password);
    }
}