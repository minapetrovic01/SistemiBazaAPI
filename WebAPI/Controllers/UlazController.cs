using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UlazController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiUlaze/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUlaze([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajUlaze(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajUlaz/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajUlaz([FromBody] UlazPregled u, [FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                DataProvider.DodajUlaz(u, id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("IzmeniUlaz")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniUlaz([FromBody] UlazPregled u)
        {
            try
            {
                DataProvider.IzmeniUlaz(u);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiUlaz/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiUlaz([FromRoute(Name ="id")] int id)
        {
            try
            {
                DataProvider.ObrisiUlaz(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
