
using AutoMapper;
using EscNet.Cryptography.Interfaces;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;

namespace Manager.Services.Services
{
    public class UserService : IUserService
    {
        public IMapper _mapper;
        public IUserRepository _userRepository;
        private readonly IRijndaelCryptography _rijndaelCryptography;

        public UserService(IMapper mapper, IUserRepository userRepository, IRijndaelCryptography iRijndaelCryptography)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _rijndaelCryptography = iRijndaelCryptography;
        }

        public async Task<UserDto> Create(UserDto userDto)
        {
            var userExist = await _userRepository.GetByEmail(userDto.Email);

            if (userExist != null)
                throw new DomainException("Já existe um usuario cadastrado com o email informado!");

            var user = _mapper.Map<User>(userDto);
            user.Validate();

            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDto>(userCreated);
        }

        public async Task<UserDto> Update(UserDto userDto)
        {
            var userExist = await _userRepository.Get(userDto.Id);

            if (userExist == null)
                throw new DomainException("Usuario não encontrado!");

            var user = _mapper.Map<User>(userDto);
            user.Validate();

            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userCreated = await _userRepository.Update(user);

            return _mapper.Map<UserDto>(userCreated);
        }

        public async Task Remove(long id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<UserDto> Get(long id)
        {
            var user = await _userRepository.Get(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> Get()
        {
            var allUser = await _userRepository.Get();

            return _mapper.Map<List<UserDto>>(allUser);
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> SearchByEmail(string email)
        {
            var allUser = await _userRepository.SearchByEmail(email);

            return _mapper.Map<List<UserDto>>(allUser);
        }

        public async Task<List<UserDto>> SearchByName(string name)
        {
            var allUser = await _userRepository.SearchByName(name);

            return _mapper.Map<List<UserDto>>(allUser);
        }

    }
}
