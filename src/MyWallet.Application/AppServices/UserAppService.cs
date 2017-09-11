using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Contracts;

namespace MyWallet.Application.AppServices
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserAppService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(UserRegistrationDto userDto)
        {
            if ((await _userRepository.FindByEmailAsync(userDto.Email)) != null)
                throw new Exception($"This e-mail '{userDto.Email}' already exists in our system");

            var user = _mapper.Map<User>(userDto);

            _userRepository.Add(user);

        }

        public UserDto Authenticate(UserLoginDto userDto)
        {
            var user = _userRepository.Find(u => u.Password.Equals(userDto.Password) && u.Email.Equals(userDto.Email)).Single();
            if (user == null) throw new InvalidCredentialException("e-mail/Password are wrong.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByTokenIdAsync(string token, Guid id)
        {
            var user = await _userRepository.FindByTokenIdAsync(token, id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            return _mapper.Map<UserDto>(await _userRepository.FindByEmailAsync(email));
        }
    }
}