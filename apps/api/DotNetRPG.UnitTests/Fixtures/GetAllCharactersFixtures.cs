using DotNetRPG.API.Dtos.Character;
using DotNetRPG.API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetRPG.UnitTests.Fixtures;

public static class GetAllCharactersFixtures
{
    public static List<GetCharacterDtoResponse> GetAllCharactersFixturesData() => new()
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

    public static ServiceResponse<List<GetCharacterDtoResponse>> GetAllCharactersFixturesResponse() => new()
    {
        Data = GetAllCharactersFixturesData(),
        Success = true,
        Message = ""
    };

    public static List<GetCharacterDtoResponse> GetAllCharactersFixturesDataNotFound() => new()
    {
       
    };

    public static ServiceResponse<List<GetCharacterDtoResponse>> GetAllCharactersFixturesNotFoundResponse() => new()
    {
        Data = GetAllCharactersFixturesDataNotFound(),
        Success = false,
        Message = "Character/s not found!"
    };

}
