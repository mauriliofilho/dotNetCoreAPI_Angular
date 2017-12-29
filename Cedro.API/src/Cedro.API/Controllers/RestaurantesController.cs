using Cedro.API.Entities;
using Cedro.API.Models;
using Cedro.API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Controllers
{
    [Route("api/restaurantes")]
    public class RestaurantesController : Controller
    {
        private IRestauranteInfoRepository _restauranteInfoRepository;

        public RestaurantesController(IRestauranteInfoRepository restauranteIntoRepository)
        {
            _restauranteInfoRepository = restauranteIntoRepository;
        }

        [HttpGet()]
        public IActionResult GetRestaurantes()
        {
            var restauranteEntities = _restauranteInfoRepository.GetRestaurantes();

            var results = Mapper.Map<IEnumerable<RestaurantesSemPratosDto>>(restauranteEntities);

            return Ok(results);
        }

        [HttpGet("{id}", Name = "GetRestaurante")]
        public IActionResult GetRestaurante(int id, bool includePratosRestaurante = false)
        {
            var restaurante = _restauranteInfoRepository.GetRestaurante(id, includePratosRestaurante);

            if (restaurante== null )
            {
                return NotFound();
            }

            if (includePratosRestaurante)
            {
                var restauranteResult = Mapper.Map<RestauranteDto>(restaurante);
                return Ok(restauranteResult);
            }

            var restaurantesSemPratos = Mapper.Map<RestaurantesSemPratosDto>(restaurante);
            return Ok(restaurantesSemPratos);

        }

        [HttpPost()]
        public IActionResult AddRestaurante([FromBody] Restaurantes restaurante)
        {
            if (restaurante == null)
            {
                return BadRequest();
            }

            if (restaurante.Descricao == restaurante.Nome)
            {
                ModelState.AddModelError("Descricao", "Erro nao deve ser igula");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _restauranteInfoRepository.AddRestaurante(restaurante);


            if (!_restauranteInfoRepository.Save())
            {
                return StatusCode(500, "Erro ao salvar");
            }

            var restauranteReturn = Mapper.Map<Models.RestauranteDto>(restaurante);

            return CreatedAtRoute("GetRestaurante", new
            { restaurante = restaurante.Id, id = restauranteReturn.Id}, restauranteReturn);

        }


        [HttpPut("{restauranteid}")]
        public IActionResult UpdateRestaurante(int restauranteId, [FromBody] RestauranteForUpdateDto restaurante)
        {
            if (restaurante == null)
            {
                return BadRequest();
            }

            if (restaurante.Descricao == restaurante.Nome)
            {
                ModelState.AddModelError("Descricao", "Erro nao pode ser igual");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_restauranteInfoRepository.RestauranteExiste(restauranteId))
            {
                return NotFound();
            }

            var restauranteEntity = _restauranteInfoRepository.GetRestaurante(restauranteId, false);
            if (restauranteEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(restaurante, restauranteEntity);

            if (!_restauranteInfoRepository.Save())
            {
                return StatusCode(500, "Erro ao Salvar");
            }

            return NoContent();
        }

        [HttpDelete("{restauranteid}")]
        public IActionResult DeleteRestaurante(int restauranteId)
        {
            if (!_restauranteInfoRepository.RestauranteExiste(restauranteId))
            {
                return NotFound();
            }

            var restauranteEntity = _restauranteInfoRepository.GetRestaurante(restauranteId, false);
            if (restauranteEntity == null)
            {
                return NotFound();
            }
            //Fazer operação para remoção dos patos caso exixtam!
            _restauranteInfoRepository.DeleteRestaurante(restauranteEntity);
            if (!_restauranteInfoRepository.Save())
            {
                return StatusCode(500, "Erro ao salvar");
            }

            return NoContent();


        }
    }
}
