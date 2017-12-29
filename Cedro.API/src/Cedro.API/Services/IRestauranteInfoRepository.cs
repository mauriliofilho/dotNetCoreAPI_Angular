using Cedro.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Services
{
    public interface IRestauranteInfoRepository
    {
        bool RestauranteExiste(int restauranteId);
        IEnumerable<Restaurantes> GetRestaurantes();
        Restaurantes GetRestaurante(int restauranteId, bool includePratosRestaurante);

        IEnumerable<PratosRestaurantes> GetPratosRestauranteByRest(int restauranteId);

        PratosRestaurantes GetPratoRestauranteByRest(int restauranteId, int pratosRestauranteId);

        void AddRestaurante(Restaurantes restaurante);

        void AddPratosForRestaurante(int restauranteId, PratosRestaurantes pratosRestaurante);

        void DeletePratosforRestaurantes(PratosRestaurantes pratosRestaurante);

        void DeleteRestaurante(Restaurantes restaurante);

        bool Save();


    }
}
