using PasseiosComCaes.Models;

namespace PasseiosComCaes.Interfaces;

public interface IClubRepository
{
    Task<IEnumerable<Club>> GetALlClubsAsync();
    Task<Club>  GetClubIdAsync(int  id);
    Task<Club> GetClubIdAsyncNoTracking(int id);
    Task<IEnumerable<Club>> GetClubByCidade(string cidade); 
    bool CriarClub(Club club);
    bool UpdateClub(Club club);
    bool DeleteClub(Club club);
    bool Save();
}