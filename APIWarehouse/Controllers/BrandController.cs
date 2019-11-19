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
    public class BrandController : ControllerBase
    {
        private readonly IBrandDomain _domain;

        public BrandController(IBrandDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BrandOut>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListAll()
        {
            var lista = _domain.ListAll();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrandOut), StatusCodes.Status200OK)]
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
        public IActionResult Post([FromBody] BrandIn brandIn)
        {
            try
            {
                _domain.Add(brandIn);
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
        public IActionResult Put([FromBody] BrandIn brandIn)
        {
            try
            {
                _domain.Update(brandIn);
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
    }
}
