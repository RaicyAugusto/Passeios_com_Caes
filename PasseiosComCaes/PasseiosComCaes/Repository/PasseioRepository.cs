using Microsoft.EntityFrameworkCore;
using PasseiosComCaes.Data;
using PasseiosComCaes.Interfaces;
using PasseiosComCaes.Models;

namespace PasseiosComCaes.Repository
{
    public class PasseioRepository : IPasseioRepository
    {
        private readonly AppDataContext _context;

        public PasseioRepository(AppDataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Passeio>> GetAllPasseiosAsync()
        {
            return await _context.Passeios.ToListAsync();
        }

        public async Task<Passeio> GetPasseioByIdAsync(int id)
        {
            return await _context.Passeios.Include(a=>a.Endereco).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Passeio> GetPasseioByIdAsyncNoTracking(int id)
        {
            return await _context.Passeios.AsNoTracking().Include(a => a.Endereco).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Passeio>> GetPasseioByCidade(string cidade)
        {
            return await _context.Passeios.Where(x => x.Endereco.Cidade.Contains(cidade)).ToListAsync();
        }
        public bool CriarPasseio(Passeio passeio)
        {
            _context.Passeios.Add(passeio);
            return Save();
        }

        public bool UpdatePasseio(Passeio passeio)
        {
            _context.Passeios.Update(passeio);
            return Save();
        }

        public bool DeletePasseio(Passeio passeio)
        {
            _context.Passeios.Remove(passeio);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
