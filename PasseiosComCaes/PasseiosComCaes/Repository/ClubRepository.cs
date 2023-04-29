using Microsoft.EntityFrameworkCore;
using PasseiosComCaes.Data;
using PasseiosComCaes.Interfaces;
using PasseiosComCaes.Models;

namespace PasseiosComCaes.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly AppDataContext _context;

        public ClubRepository(AppDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Club>> GetALlClubsAsync()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetClubIdAsync(int id)
        {
            return await _context.Clubs.Include(a => a.Endereco).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Club> GetClubIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.AsNoTracking().Include(a => a.Endereco).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCidade(string cidade)
        {
            return await _context.Clubs.Where(x => x.Endereco.Cidade.Contains(cidade)).ToListAsync();
        }

        public bool CriarClub(Club club)
        {
            _context.Clubs.Add(club);
            return Save();
        }

        public bool UpdateClub(Club club)
        {
            _context.Clubs.Update(club);
            return Save();
        }

        public bool DeleteClub(Club club)
        {
            _context.Clubs.Remove(club);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
