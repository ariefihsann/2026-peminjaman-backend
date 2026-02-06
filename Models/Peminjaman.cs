using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Peminjaman.Backend.Models
{
    public class PeminjamanRuangan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NamaPeminjam { get; set; } = string.Empty;

        [Required]
        public string NamaRuangan { get; set; } = string.Empty;

        [Required]
        public DateTime TanggalPeminjaman { get; set; }

        public string Keperluan { get; set; } = string.Empty;

        public string Status { get; set; } = "menunggu";
    }

}