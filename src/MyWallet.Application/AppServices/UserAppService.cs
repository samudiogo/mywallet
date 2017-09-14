using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.DataModels;

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
            try
            {
                if (await _userRepository.FindByEmailAsync(userDto.Email) != null)
                    throw new Exception($"This e-mail '{userDto.Email}' already exists in our system");

                var user = _mapper.Map<UserDataModel>(userDto);

                _userRepository.Add(user);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public UserDto Authenticate(UserLoginDto userDto)
        {
            var userCriteria = _mapper.Map<User>(userDto);
            var user = _userRepository.Find(u => u.Password.Equals(userCriteria.Password) && u.Email.Equals(userCriteria.Email)).FirstOrDefault();
            if (user == null) throw new InvalidCredentialException("e-mail/Password are wrong.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByTokenAsync(string token)
        {
            var user = await _userRepository.FindByTokenAsync(token);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            return _mapper.Map<UserDto>(await _userRepository.FindByEmailAsync(email));
        }
    }
}