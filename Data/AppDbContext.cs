using Microsoft.EntityFrameworkCore;
using Peminjaman.Backend.Models;

namespace Peminjaman.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<PeminjamanRuangan> Peminjamans { get; set; }
        public DbSet<User> Users { get; set; }

    }
}