using System.ComponentModel.DataAnnotations;
namespace RestorantDeneme.Models

{
    public class Galeri
    {
        [Key]
        public int Id { get; set; }

        public string Image { get; set; }
    }
}
