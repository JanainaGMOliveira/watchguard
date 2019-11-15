using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWarehouse.Repository.Interface;
using Infra.DTO;
using Infra.Filtros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIWarehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromQuery] ProductFilter filtro)
        {
            var lista = _repo.Get(filtro);

            return Ok(lista);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetById([FromRoute] long id)
        {
            var resp = _repo.GetById(id);

            if (resp != null)
                return Ok(resp);

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostAtribuicaoBeneficio([FromBody] ProductDTO productDTO)
        {
            try
            {
                _repo.Add(productDTO);
                return Ok();
            }
            catch (Exception e)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = e.Message });
                return result;
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PutAtribuicaoBeneficio([FromBody] ProductDTO productDTO)
        {
            try
            {
                _repo.Update(productDTO);
                return Ok();
            }
            catch (Exception e)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = e.Message });
                return result;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _repo.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = e.Message });
                return result;
            }
        }
    }
}
