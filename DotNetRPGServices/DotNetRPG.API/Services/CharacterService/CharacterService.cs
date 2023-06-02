using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DotNetRPG.API.Data;
using DotNetRPG.API.Dtos.Character;

namespace DotNetRPG.API.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public ServiceResponse<List<GetCharacterDtoResponse>> GetAllCharacters()
        {
            // throw new NotImplementedException();
            // return new ServiceResponse<List<GetCharacterDto>> { 
            //     Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            // };

            var response = new ServiceResponse<List<GetCharacterDtoResponse>>();

            try
            {
                var dbCharacter = _context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDtoResponse>(c))
                    .ToList();
                var sortedCharacterList = dbCharacter
                    .OrderBy(c => c.Id).ToList();
                response.Data = sortedCharacterList;
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }


            return response;
        }

        public ServiceResponse<GetCharacterDtoResponse> GetCharacterById(GetSingleCharacterRequest singleCharacterRequest)
        {
            // throw new NotImplementedException();
            var serviceResponse = new ServiceResponse<GetCharacterDtoResponse>();

            try
            {
                var dbCharacter = _context.Characters
                    .FirstOrDefault(c => c.Id == singleCharacterRequest.Id && c.User.Id == GetUserId());
                serviceResponse.Data = _mapper.Map<GetCharacterDtoResponse>(dbCharacter);
    
                //If Characater not found
                if(serviceResponse.Data == null){
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character Not Found!";
                }
            }
            catch (Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exc.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDtoResponse>>> AddCharacter(AddCharacterDtoRequest newCharacter)
        {
            // throw new NotImplementedException();
            // var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // Character character = _mapper.Map<Character>(newCharacter);
            // character.Id = characters.Max(c => c.Id) + 1;
            // characters.Add(character);
            // serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            // return serviceResponse;

            ServiceResponse<List<GetCharacterDtoResponse>> serviceResponse = new ServiceResponse<List<GetCharacterDtoResponse>>();

            try
            {
                //Adding character via data context
                Character character = _mapper.Map<Character>(newCharacter);
                character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

                _context.Characters.Add(character);
                await _context.SaveChangesAsync();//saving changes to database

                //display characters that belongs to the current user 
                var dbCharacter = await _context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDtoResponse>(c))
                    .ToListAsync();
                var sortedCharacterList = dbCharacter
                    .OrderBy(c => c.Id).ToList();
                serviceResponse.Data = sortedCharacterList;
            }
            catch (Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exc.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDtoResponse>> UpdateCharacter(UpdateCharacterDtoRequest updatedCharacter)
        {
            ServiceResponse<GetCharacterDtoResponse> response = new ServiceResponse<GetCharacterDtoResponse>();
            
            try{// If the character exist
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                //Map(Source, target)
                // _mapper.Map(updatedCharacter, character);
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class; 

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDtoResponse>(character);
                response.Message = "Charactere ID:" + character.Id + " has been updated";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDtoResponse>>> DeleteCharacter(DeleteCharacterRequest deleteCharacterRequest)
        {
            ServiceResponse<List<GetCharacterDtoResponse>> response = new ServiceResponse<List<GetCharacterDtoResponse>>();

            try
            {// If the character exist
                Character character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == deleteCharacterRequest.Id && c.User.Id == GetUserId());

                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();

                    var dbCharacter = await _context.Characters
                        .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDtoResponse>(c))
                        .ToListAsync();
                    response.Data = dbCharacter
                        .OrderBy(c => c.Id).ToList();

                    response.Message = "Character ID:" + deleteCharacterRequest.Id + " has been deleted";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character not found!";
                }

            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }

            return response;
        }
    }
}