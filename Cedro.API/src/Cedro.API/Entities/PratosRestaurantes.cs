using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cedro.API.Entities
{
    public class PratosRestaurantes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [MaxLength(200)]
        public string Descricao { get; set; }

        [ForeignKey("RestaurantesId")]
        public Restaurantes Restaurantes { get; set; }
        public int RestaurantesId { get; set; }

    }
}