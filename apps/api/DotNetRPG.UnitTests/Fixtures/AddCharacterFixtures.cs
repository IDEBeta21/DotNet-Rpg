using DotNetRPG.API.Dtos.Character;
using DotNetRPG.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetRPG.UnitTests.Fixtures;


public static class AddCharacterFixtures
{
    public static List<GetCharacterDtoResponse> AddCharacterFixturesData() => new()
    {
        new()
        {
            Id = 31,
            Name = "Evelyn",
            HitPoints = 100,
            Strength = 5,
            Defense = 5,
            Intelligence = 10,
            Class = RpgClass.Knight
        },
        new()
        {
            Id = 32,
            Name = "Celestine",
            HitPoints = 100,
            Strength = 5,
            Defense = 5,
            Intelligence = 10,
            Class = RpgClass.Mage
        }
    };

    public static ServiceResponse<List<GetCharacterDtoResponse>> AddCharacterFixturesResponse() => new()
    {
        Data = AddCharacterFixturesData(),
        Success = true,
        Message = ""
    };

}

