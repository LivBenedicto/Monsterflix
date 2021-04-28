using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monsterflix.Api.Data;
using Monsterflix.Api.Models;
using Monsterflix.Api.Repositories.Contracts;

namespace Monsterflix.Api.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;

        public ProfileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Profile> CreateNewProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<IList<Profile>> GetListProfiles()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile> GetProfileById(int idProfile)
        {
            return await _context.Profiles.Where(profile => profile.IdProfile == idProfile).FirstOrDefaultAsync();
        }

        public async Task<Profile> DeleteProfile(int idProfile)
        {
            Profile profileToRemove = _context.Profiles.Find(idProfile);
            _context.Profiles.Remove(profileToRemove);
            await _context.SaveChangesAsync();

            return profileToRemove;
        }
    }
}