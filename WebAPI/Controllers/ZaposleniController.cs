using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZaposleniController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiZaposlene")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetZaposlene()
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajZaposlene());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiZaposlenog/{idZaposlenog}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetZaposlenog([FromRoute(Name ="idZaposlenog")] string id)
        {
            try
            {
                return new JsonResult(DataProvider.PreuzmiZaposlenog(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiUpravnike")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUpravnike()
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajUpravnike());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiZaposlenog/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteZaposlenog([FromRoute(Name ="id")] string id)
        {
            try
            {
                DataProvider.ObrisiZaposlenog(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajZaposlenog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddZaposlenog([FromBody] ZaposleniPregled z)
        {
            try
            {
                DataProvider.DodajZaposlenog(z);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("IzmeniZaposlenog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateZaposlenog([FromBody] ZaposleniPregled z)
        {
            try
            {
                DataProvider.IzmeniZaposlenog(z);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }

}
