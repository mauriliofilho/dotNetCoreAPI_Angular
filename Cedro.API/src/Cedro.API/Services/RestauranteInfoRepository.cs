using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cedro.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cedro.API.Services
{
    public class RestauranteInfoRepository : IRestauranteInfoRepository
    {
        private ConnectContext _context;

        public RestauranteInfoRepository(ConnectContext context)
        {
            _context = context;
        }


        public bool RestauranteExiste(int restauranteId)
        {
            return _context.Restaurantes.Any(r => r.Id == restauranteId);
        }

        public Restaurantes GetRestaurante(int restauranteId, bool includePratosRestaurante)
        {
            if (includePratosRestaurante)
            {
                return _context.Restaurantes.Include(r => r.PratosRestaurantes)
                        .Where(r => r.Id == restauranteId).FirstOrDefault();
            }

            return _context.Restaurantes.Where(r => r.Id == restauranteId).FirstOrDefault();
        }

        public PratosRestaurantes GetPratoRestauranteByRest(int restauranteId, int pratosRestauranteId)
        {
            return _context.PratosRestaurantes.Where(p => p.RestaurantesId == restauranteId && p.Id == pratosRestauranteId).FirstOrDefault();
        }

        public IEnumerable<PratosRestaurantes> GetPratosRestauranteByRest(int restauranteId)
        {
            return _context.PratosRestaurantes.Where(p => p.RestaurantesId == restauranteId).ToList();
        }


        public IEnumerable<Restaurantes> GetRestaurantes()
        {
            return _context.Restaurantes.OrderBy(r => r.Id).ToList();
        }

        public void AddRestaurante(Restaurantes restaurante)
        {
            _context.Restaurantes.Add(restaurante);
        }

        public void AddPratosForRestaurante(int restauranteId, PratosRestaurantes pratosRestaurante)
        {
            var restaurante = GetRestaurante(restauranteId, false);
            restaurante.PratosRestaurantes.Add(pratosRestaurante);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeletePratosforRestaurantes(PratosRestaurantes pratosRestaurante)
        {
            _context.PratosRestaurantes.Remove(pratosRestaurante);
        }

        public void DeleteRestaurante(Restaurantes restaurante)
        {
            _context.Restaurantes.Remove(restaurante);
        }

    }
}
