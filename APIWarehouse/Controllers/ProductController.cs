using System;
using System.Collections.Generic;
using Infra.DTO.Ins;
using Infra.DTO.Outs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIWarehouse.Domains.Interface;

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
        public IActionResult ListAll()
        {
            var lista = _domain.ListAll();

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
        public IActionResult Post([FromBody] ProductIn productIn)
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
        public IActionResult Put([FromBody] ProductIn productIn)
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

        [HttpGet("ProductsByBrand")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FileWithProductsByBrand()
        {
            var doc = _domain.FileWithProductsByBrand();

            return Ok("The path of saved file is " + doc);
        }
    }
}
