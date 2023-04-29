using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PasseiosComCaes.Models;

namespace PasseiosComCaes.Data
{
    public class AppDataContext : IdentityDbContext<AppUser>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
            
        }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Passeio> Passeios { get; set; }

    }
}
