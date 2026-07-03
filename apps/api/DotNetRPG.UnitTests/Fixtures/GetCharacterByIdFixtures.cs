using DotNetRPG.API.Dtos.Character;
using DotNetRPG.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetRPG.UnitTests.Fixtures;


public static class GetCharacterByIdFixtures
{
    public static GetCharacterDtoResponse GetCharacterByIdData() => new()
    {
        Id = 31,
        Name = "Evelyn",
        HitPoints = 100,
        Strength = 5,
        Defense = 5,
        Intelligence = 10,
        Class = RpgClass.Knight
    };

    public static ServiceResponse<GetCharacterDtoResponse> GetCharacterByIdResponse() => new()
    {
        Data = GetCharacterByIdData(),
        Success = true,
        Message = ""
    };

    public static ServiceResponse<GetCharacterDtoResponse> GetCharacterByIdNotFoundResponse() => new()
    {
        Data = null,
        Success = false,
        Message = "Character Not Found!"
    };
}
