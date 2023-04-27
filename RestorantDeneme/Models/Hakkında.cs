using System.ComponentModel.DataAnnotations;

namespace RestorantDeneme.Models
{
    public class Hakkında
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
    }
}
