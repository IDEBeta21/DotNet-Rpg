using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetRPG.API.Dtos.Character;
using DotNetRPG.API.Services.CharacterService;

namespace DotNetRPG.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {   
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
             _characterService = characterService;
        }
        
        [HttpGet("GetAllCharacters")]// Returs all the Characters
        public async Task<IActionResult> GetAllCharacters(){

            ServiceResponse<List<GetCharacterDtoResponse>> response = _characterService.GetAllCharacters();

            if(response.Data.Count == 0)
            {
                response.Success = false;
                response.Message = "Character/s not found!";
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("GetCharacterById")]// Returns a single character with id provided 
        public async Task<IActionResult> GetCharacterById(GetSingleCharacterRequest singleCharacterRequest){
            var response = _characterService.GetCharacterById(singleCharacterRequest);
            if(response.Data == null) return NotFound(response);
            return Ok(response);
        }

        [HttpPost("AddCharacter")]//Add new Character
        public async Task<IActionResult> AddCharacter(AddCharacterDtoRequest newCharacter){
            return Ok(_characterService.AddCharacter(newCharacter));
        }

        [HttpPut("UpdateCharacter")]//Update character information
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDtoRequest updatedCharacter){
            var response = _characterService.UpdateCharacter(updatedCharacter);
            if(response.Data == null){
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteCharacterById")]// Returns a single character with id provided 
        public async Task<IActionResult> DeleteCharacterById(DeleteCharacterRequest deleteCharacterRequest){
            var response = _characterService.DeleteCharacterById(deleteCharacterRequest);
            if(response.Data == null){
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}