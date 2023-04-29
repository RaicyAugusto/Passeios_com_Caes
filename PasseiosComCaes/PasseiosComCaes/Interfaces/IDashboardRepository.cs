using PasseiosComCaes.Models;

namespace PasseiosComCaes.Interfaces;

public interface IDashboardRepository
{
    Task<List<Passeio>> GetAllUsuarioPasseio();
    Task<List<Club>> GetAllUsuarioClub();
}