using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Models
{
    public class PratosRestaurantesForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(200)]
        public string Descricao { get; set; }
    }
}
