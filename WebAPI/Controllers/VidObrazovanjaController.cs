using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VidObrazovanjaController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiVidoveObrazovanja/{idZaposlenog}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVidoveObrazovanja([FromRoute(Name ="idZaposlenog")]string idZaposlenog)
        {
            try
            {
                return new JsonResult(DataProvider.VratiVidoveObrazovanja(idZaposlenog));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajVidObrazovanja/{idZaposlenog}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajVidObrazovanja([FromBody] VidObrazovanjaPregled v, [FromRoute(Name ="idZaposlenog")]string idZaposlenog)
        {
            try
            {
                DataProvider.DodajVidObrazovanja(v, idZaposlenog);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("IzmeniVidObrazovanja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniVidObrazovanja([FromBody] VidObrazovanjaPregled v)
        {
            try
            {
                DataProvider.IzmeniVidObrazovanja(v);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiVidObrazovanja/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiVidObrazovanja([FromRoute(Name ="id")]int id)
        {
            try
            {
                DataProvider.ObrisiVidObrazovanja(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiVidObrazovanja/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVidObrazovanja([FromRoute(Name ="id")]int id)
        {
            try
            {
                return new JsonResult(DataProvider.PreuzmiVidObrazovanja(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
