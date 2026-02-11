using Microsoft.EntityFrameworkCore;
using Peminjaman.Backend.Models;

namespace _2026_peminjaman_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PeminjamanRuangan> PeminjamanRuangan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PeminjamanRuangan>().HasData(
                new PeminjamanRuangan
                {
                    Id = 1,
                    NamaPeminjam = "Admin Default",
                    NamaRuangan = "Lab Komputer 1",
                    TanggalPeminjaman = DateTime.Now,
                    Keperluan = "Maintenance",
                    Status = "disetujui"
                },
                new PeminjamanRuangan
                {
                    Id = 2,
                    NamaPeminjam = "Dosen Tamu",
                    NamaRuangan = "Aula Utama",
                    TanggalPeminjaman = DateTime.Now.AddDays(1),
                    Keperluan = "Seminar",
                    Status = "menunggu"
                }
            );
        }
    }
}