using Cedro.API.Models;
using Cedro.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Controllers
{
    [Route("api/restaurantes")]
    public class PratosRestaurantesController : Controller
    {
        private IRestauranteInfoRepository _restauranteInfoRepository;
        public PratosRestaurantesController(IRestauranteInfoRepository restauranteInfoRepository)
        {
            _restauranteInfoRepository = restauranteInfoRepository;
        }

        [HttpGet("{restauranteId}/pratosrestaurante", Name = "GetPratosRestaurante")]
        public IActionResult GetPratosRestaurantes(int restauranteId)
        {
            try
            {
                if (!_restauranteInfoRepository.RestauranteExiste(restauranteId))
                {
                    return NotFound();
                }

                var pratosrRestaurantesPorRest = _restauranteInfoRepository.GetPratosRestauranteByRest(restauranteId);

                var pratosRestaurantesPorRestResult = Mapper.Map<IEnumerable<PratosRestaurantesDto>>(pratosrRestaurantesPorRest);

                return Ok(pratosRestaurantesPorRestResult);

            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro");
            }

            return null;
        }


        [HttpGet("{restauranteId}/pratosrestaurante/{id}", Name = "GetRestaurantes")]
        public IActionResult GetPratosRestaurante(int restauranteId, int Id)
        {
            if (!_restauranteInfoRepository.RestauranteExiste(restauranteId))
            {
                return NotFound();
            }

            var pratosRestaurantes = _restauranteInfoRepository.GetPratoRestauranteByRest(restauranteId, Id);
            if (pratosRestaurantes == null)
            {
                return NotFound();
            }

            var pratosRestaurantesResult = Mapper.Map<PratosRestaurantesDto>(pratosRestaurantes);

            return Ok(pratosRestaurantesResult);

        }

        [HttpPost("{restauranteId}/pratosrestaurante")]
        public IActionResult CreatePratoRestaurante(int restauranteId, [FromBody] PratosRestaurantesDto pratosRestaurante)
        {
            if (pratosRestaurante == null)
            {
                return BadRequest();
            }

            if (pratosRestaurante.Descricao == pratosRestaurante.Nome)
            {
                ModelState.AddModelError("Descrição", "A descrição do item deve ser diferente do Nome");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_restauranteInfoRepository.RestauranteExiste(restauranteId))
            {
                return NotFound();
            }

            var finalPratosRestaurante = Mapper.Map<Entities.PratosRestaurantes>(pratosRestaurante);

            _restauranteInfoRepository.AddPratosForRestaurante(restauranteId, finalPratosRestaurante);

            if (!_restauranteInfoRepository.Save())
            {
                return StatusCode(500, "Erro ao gravar");
            }

            var createPratosRestauranteToReturn = Mapper.Map<Models.PratosRestaurantesDto>(finalPratosRestaurante);

            return CreatedAtRoute("GetPratosRestaurante", new
            { restauranteId = restauranteId, id = createPratosRestauranteToReturn.Id }, createPratosRestauranteToReturn);

        }


        [HttpPut("{restauranteId}/pratosrestaurante/{id}")]
        public IActionResult UpdatePratosRestaurente(int restauranteId, int id,
            [FromBody] PratosRestaurantesForUpdateDto pratosRestaurante)
        {
            if (pratosRestaurante == null)
            {
                return BadRequest();
            }

            if (pratosRestaurante.Descricao == pratosRestaurante.Nome)
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

            var pratosRestaurantesEntity = _restauranteInfoRepository.GetPratoRestauranteByRest(restauranteId, id);
            if (pratosRestaurantesEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(pratosRestaurante, pratosRestaurantesEntity);

            if (!_restauranteInfoRepository.Save())
            {
                return StatusCode(500, "Erro problema ocorrido");
            }

            return NoContent();
        }


        [HttpDelete("{restauranteId}/pratosrestaurante/{id}")]
        public IActionResult DeletePratosRestaurante(int restauranteId, int id)
        {
            if (!_restauranteInfoRepository.RestauranteExiste(restauranteId))
            {
                return NotFound();
            }

            var pratosRestauranteEntity = _restauranteInfoRepository.GetPratoRestauranteByRest(restauranteId, id);
            if (pratosRestauranteEntity == null)
            {
                return NotFound();
            }

            _restauranteInfoRepository.DeletePratosforRestaurantes(pratosRestauranteEntity);

            if (!_restauranteInfoRepository.Save())
            {
                return StatusCode(500, "Erro ao Deletar");
            }

            return NoContent();


        }




    }
}
