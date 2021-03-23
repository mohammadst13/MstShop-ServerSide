using MstShop_ServerSide.Core.DTOs.Account;
using MstShop_ServerSide.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MstShop_ServerSide.Core.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsers();

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        bool IsUserExistsByEmail(string email);
        Task<LoginUserResult> LoginUser(LoginUserDTO login);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserId(long userId);
    }
}
