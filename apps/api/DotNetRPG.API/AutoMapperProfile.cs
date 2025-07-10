using AutoMapper;
using DotNetRPG.API.Dtos.Character;

namespace MYAPP
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDtoResponse>();
            CreateMap<AddCharacterDtoRequest, Character>();
            CreateMap<UpdateCharacterDtoRequest, Character>();
        }
    }
}