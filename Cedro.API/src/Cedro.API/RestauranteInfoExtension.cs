using Cedro.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API
{
    public static class RestauranteInfoExtension
    {
        public static void EnsureSeedDataForContext(this ConnectContext context)
        {
            if (context.Restaurantes.Any())
            {
                return; 
            }


            var restaurantes = new List<Restaurantes>()
            {
                new Restaurantes()
                {
                    Nome = "La petiti",
                    Descricao = "Restaurante Italiano",
                    PratosRestaurantes = new List<PratosRestaurantes>()
                    {
                        new PratosRestaurantes()
                        {
                            Nome = "Cebolada",
                            Descricao = "Prato com Bastante Cebola"
                        },
                        new PratosRestaurantes()
                        {
                            Nome = "Muqueca de farinha",
                            Descricao = "Prato com pasta de farinha"
                        },
                    }

                },
                new Restaurantes()
                {
                    Nome = "Baruque",
                    Descricao = "Bar e Chperia Chique",
                    PratosRestaurantes = new List<PratosRestaurantes>()
                    {
                        new PratosRestaurantes()
                        {
                            Nome = "Filezino na Chapa",
                            Descricao = "File de frango pequeno na frigideira"
                        },
                        new PratosRestaurantes()
                        {
                            Nome = "Bananada",
                            Descricao = "Doce de Banana"
                        },
                    }
                }
            };

            context.AddRange(restaurantes);
            context.SaveChanges();
        }
    }
}
