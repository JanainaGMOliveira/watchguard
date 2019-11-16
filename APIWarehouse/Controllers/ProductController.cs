using System;
using System.Collections.Generic;
using APIWarehouse.Domains.Interface;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIWarehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductDomain _domain;

        public ProductController(IProductDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductOut>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListAll([FromQuery] bool? filtroAtivo)
        {
            var lista = _domain.ListAll(filtroAtivo);

            return Ok(lista);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetById([FromRoute] long id)
        {
            var resp = _domain.GetById(id);

            if (resp != null)
                return Ok(resp);

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostAtribuicaoBeneficio([FromBody] ProductIn productIn)
        {
            try
            {
                _domain.Add(productIn);
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
        public IActionResult PutAtribuicaoBeneficio([FromBody] ProductIn productIn)
        {
            try
            {
                _domain.Update(productIn);
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
                _domain.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = e.Message });
                return result;
            }
        }
    
        [HttpGet("QuantityActiveProducts")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FileWithActiveProducts()
        {
            var doc = _domain.FileWithActiveProducts();

            return Ok("The path of saved file is " + doc);
        }
    }
}
