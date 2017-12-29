using Cedro.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Models
{
    public class RestauranteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public int NumeroPratosRestaurantes
        {
            get
            {
                return PratosRestaurantes.Count;
            }
        }

        public ICollection<PratosRestaurantes> PratosRestaurantes { get; set; }
        = new List<PratosRestaurantes>();
    }
}
