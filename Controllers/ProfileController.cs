// Classe que controla acesso perfil

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monsterflix.Api.Models;
using Monsterflix.Api.Repositories.Contracts;

namespace Monsterflix.Api.Controllers
{
    [Route("v1/profiles")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        
        [HttpPost]
        public async Task<Profile> PostCreateNewProfile(Profile profile)
        {
            return await _profileRepository.CreateNewProfile(profile);
        }

        [HttpGet("{idProfile}")]
        public async Task<Profile> GetProfileById(int idProfile)
        {
            return await _profileRepository.GetProfileById(idProfile);
        }

        [HttpGet]
        public async Task<IList<Profile>> GetListProfiles()
        {
            return await _profileRepository.GetListProfiles();
        }

        [HttpDelete]
        public async Task<Profile> DeleteProfile(int idProfile)
        {
            return await _profileRepository.DeleteProfile(idProfile);
        }
    }
}