using Microsoft.EntityFrameworkCore;
using PasseiosComCaes.Data;
using PasseiosComCaes.Interfaces;
using PasseiosComCaes.Models;

namespace PasseiosComCaes.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(AppDataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<List<Passeio>> GetAllUsuarioPasseio()
    {
        var curUser = _httpContextAccessor.HttpContext?.User;
        var userPasseio = _context.Passeios.Where(r => r.AppUser.Id == curUser.ToString());
        return userPasseio.ToListAsync();
    }

    public Task<List<Club>> GetAllUsuarioClub()
    {
        var curUser = _httpContextAccessor.HttpContext?.User;
        var userClub = _context.Clubs.Where(r => r.AppUser.Id == curUser.ToString());
        return userClub.ToListAsync();
    }
}