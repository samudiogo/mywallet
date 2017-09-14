using System;
using System.Threading.Tasks;
using MyWallet.Application.Dto;

namespace MyWallet.Application.Contracts
{
    public interface IUserAppService
    {
        Task RegisterAsync(UserSaveOrUpdateDto userDto);
        UserDto Authenticate(UserLoginDto userDto);

        Task<UserDto> GetUserByTokenAsync(string token);

        Task<UserDto> GetUserByEmailAsync(string email);
    }
}