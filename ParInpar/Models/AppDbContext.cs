using Microsoft.EntityFrameworkCore;
using ParInpar.Models;

namespace ParInpar.Models

{
   public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PalabraVerificada> PalabrasVerificadas { get; set; }
    public DbSet<NumeroVerificado> Numeros { get; set; }
    public DbSet<TextoCifrado> Cifrados { get; set; }
    public DbSet<Usuario> Usuarios { get; set; } // 👈 Esta es la línea nueva
}

}
