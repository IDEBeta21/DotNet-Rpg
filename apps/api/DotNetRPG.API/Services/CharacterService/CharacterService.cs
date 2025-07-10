using System.Security.Claims;
using AutoMapper;
using DotNetRPG.API.Data;
using DotNetRPG.API.Dtos.Character;

namespace DotNetRPG.API.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly RpgDataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, RpgDataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public ServiceResponse<List<GetCharacterDtoResponse>> GetAllCharacters()
        {
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

        public ServiceResponse<List<GetCharacterDtoResponse>> AddCharacter(AddCharacterDtoRequest newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDtoResponse>>();

            try
            {
                //Adding character via data context
                Character character = _mapper.Map<Character>(newCharacter);
                character.User = _context.Users.FirstOrDefault(u => u.Id == GetUserId());

                _context.Characters.Add(character);
                _context.SaveChanges();//saving changes to database

                //display characters that belongs to the current user 
                var dbCharacter = _context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDtoResponse>(c))
                    .ToList();
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

        public ServiceResponse<GetCharacterDtoResponse> UpdateCharacter(UpdateCharacterDtoRequest updatedCharacter)
        {
            ServiceResponse<GetCharacterDtoResponse> response = new ServiceResponse<GetCharacterDtoResponse>();
            
            try{// If the character exist
                var character = _context.Characters
                    .FirstOrDefault(c => c.Id == updatedCharacter.Id);

                //Map(Source, target)
                // _mapper.Map(updatedCharacter, character);
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class; 

                _context.SaveChanges();

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

        public ServiceResponse<List<GetCharacterDtoResponse>> DeleteCharacterById(DeleteCharacterRequest deleteCharacterRequest)
        {
            ServiceResponse<List<GetCharacterDtoResponse>> response = new ServiceResponse<List<GetCharacterDtoResponse>>();

            try
            {// If the character exist
                Character character = _context.Characters
                    .FirstOrDefault(c => c.Id == deleteCharacterRequest.Id && c.User.Id == GetUserId());

                if (character != null)
                {
                    _context.Characters.Remove(character);
                    _context.SaveChanges();

                    var dbCharacter = _context.Characters
                        .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDtoResponse>(c))
                        .ToList();
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