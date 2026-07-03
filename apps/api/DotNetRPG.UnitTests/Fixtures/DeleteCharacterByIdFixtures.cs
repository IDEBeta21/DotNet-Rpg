using DotNetRPG.API.Dtos.Character;
using DotNetRPG.API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetRPG.UnitTests.Fixtures;

public static class DeleteCharacterByIdFixtures
{
    public static List<GetCharacterDtoResponse> DeleteCharacterByIdFixturesData() => new()
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

    public static ServiceResponse<List<GetCharacterDtoResponse>> DeleteCharacterByIdFixturesResponse() => new()
    {
        Data = DeleteCharacterByIdFixturesData(),
        Success = true,
        Message = ""
    };

    public static ServiceResponse<List<GetCharacterDtoResponse>> DeleteCharacterByIdFixturesDataNotFound() => new()
    {
        Data = null,
        Success = false,
        Message = "Character not found!"
    };
}