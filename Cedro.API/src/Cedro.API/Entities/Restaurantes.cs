using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Entities
{
    public class Restaurantes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Nome { get; set; }
        [MaxLength(100)]
        public string  Descricao { get; set; }

        public ICollection<PratosRestaurantes> PratosRestaurantes { get; set; }
            = new List<PratosRestaurantes>();
    }
}
