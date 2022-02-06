using Manager.Services.DTO;

namespace Manager.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Create(UserDto userDto);
        Task<UserDto> Update(UserDto userDto);
        Task Remove(long id);
        Task<UserDto> Get(long id);
        Task<List<UserDto>> Get();
        Task<UserDto> GetByEmail(string email);
        Task<List<UserDto>> SearchByEmail(string email);
        Task<List<UserDto>> SearchByName(string name);
    }
}
