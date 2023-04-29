using PasseiosComCaes.Models;

namespace PasseiosComCaes.Interfaces
{
    public interface IPasseioRepository
    {
        Task<ICollection<Passeio>> GetAllPasseiosAsync();
        Task<Passeio> GetPasseioByIdAsync(int id);
        Task<Passeio> GetPasseioByIdAsyncNoTracking(int id);
        Task<IEnumerable<Passeio>> GetPasseioByCidade(string cidade);
        bool CriarPasseio(Passeio passeio);
        bool UpdatePasseio(Passeio passeio);
        bool DeletePasseio(Passeio passeio);
        bool Save();
    }
}
