using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frucol.Server.Data;
using Frucol.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Armando fonseca 2022

namespace Frucol.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductorController : ControllerBase
    {
        private readonly ProductorData _productorData;

        public ProductorController(ProductorData productorData)
        {
            this._productorData = productorData;
        }

        [HttpPost("Insertar")]
        public async Task<ActionResult> Insertar([FromBody] ProductorModel productorModel)
        {
            try
            {
                var productor = await _productorData.Insertar(productorModel);
                return Ok(productor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar([FromBody] ProductorModel productorModel)
        {
            try
            {
                var productor = await _productorData.Actualizar(productorModel);
                return Ok(productor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                var productor = await _productorData.Eliminar(id);
                return Ok(productor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Seleccionar")]
        public async Task<ActionResult> Seleccionar()
        {
            try
            {
                var productor = await _productorData.Seleccionar();
                return Ok(productor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpGet("SeleccionarId/{id}")]
        public async Task<ActionResult> SeleccionarId(int id)
        {
            try
            {
                var productor = await _productorData.SeleccionarId(id);
                return Ok(productor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}