using CentralLendingApi.Data.Dtos;
using CentralLendingApi.Data.Models;
using HMapper;
namespace CentralLendingApi.Services.Mappers
{
    public static class InitMapperConfiguration
    {
        public static void Init(IMapperAPIInitializer initializer)
        {
            initializer.Map<User, UserDto>();
            initializer.Map<UserDto, User>();
        }
    }
}
