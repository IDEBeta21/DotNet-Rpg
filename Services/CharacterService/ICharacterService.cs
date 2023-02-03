using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYAPP.Services.CharacterService
{
    public interface ICharacterService
    {
        List<Character> GetAllCharcters();
        Character GetCharacterById(int id);
        List<Character> AddCharacter(Character newCharacter);
    }
}