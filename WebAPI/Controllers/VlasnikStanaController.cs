using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VlasnikStanaController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiVlasnike")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVlasnike()
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajVlasnike());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiVlasnika/{idVlasnika}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVlasnika([FromRoute(Name ="idVlasnika")] string id)
        {
            try
            {
                return new JsonResult(DataProvider.PreuzmiVlasnika(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajVlasnika")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajVlasnika([FromBody] VlasnikStanaPregled vlasnik)
        {
            try
            {
                DataProvider.DodajVlasnika(vlasnik);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("IzmeniVlasnika")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniVlasnika([FromBody] VlasnikStanaPregled vlasnik)
        {
            try
            {
                DataProvider.IzmeniVlasnika(vlasnik);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiVlasnika/{idVlasnika}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiVlasnika([FromRoute(Name ="idVlasnika")] string id)
        {
            try
            {
                DataProvider.ObrisiVlasnika(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
