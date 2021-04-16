using System.Collections.Generic;
using System.Threading.Tasks;
using Monsterflix.Api.Models;

namespace Monsterflix.Api.Repositories.Contracts
{
    public interface IProfileRepository
    {
        Task<Profile> CreateNewProfile(Profile profile);
        Task<Profile> GetProfileById(int idProfile);
        Task<IList<Profile>> GetListProfiles();
        Task<Profile> DeleteProfile(int idProfile);
    }
}