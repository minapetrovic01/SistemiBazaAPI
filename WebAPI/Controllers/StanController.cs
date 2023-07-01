using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StanController : ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiStanove")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStanove()
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSveStanove());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiStanoveJedneZgrade/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStanoveZgrade([FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSveStanoveZgrade(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajStan/{idZgrade}/{idVlasnika}/{idSprata}/{RedniBrojStana}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajStan([FromRoute(Name = "idZgrade")] int idZgrade, [FromRoute(Name = "idVlasnika")] string idVlasnika, [FromRoute(Name = "idSprata")] int idSprata, [FromRoute(Name = "RedniBrojStana")] int RedniBrojStana)
        {
            try
            {
                DataProvider.DodajStan(idZgrade, idVlasnika, idSprata, RedniBrojStana);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiStan/{idStana}/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiStan([FromRoute(Name = "idStana")] int id, [FromRoute(Name ="idZgrade")] int idZgrade)
        {
            try
            {
                DataProvider.ObrisiStan(idZgrade,id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("IzmeniVlasnikaStana/{idStana}/{idNovogVlasnika}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniVlasnikaStana([FromRoute(Name = "idStana")] int id, [FromRoute(Name = "idNovogVlasnika")] string idNovogVlasnika)
        {
            try
            {
                DataProvider.IzmeniVlasnikaStana(id, idNovogVlasnika);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
