using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpratController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiSpratove/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSpratove([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSpratove(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiViseSpratove/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetViseSpratove([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSveViseSpratove(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiNizeSpratove/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNizeSpratove([FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSveNizeSpratove(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiPodzemneSpratove/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPodzemneSpratove([FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSvePodzemneSpratove(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajSprat/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddSprat([FromBody] SpratPregled sprat, [FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                DataProvider.DodajSprat(sprat, id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiSprat/{idSprata}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSprat([FromRoute(Name = "idSprata")] int id)
        {
            try
            {
                DataProvider.ObrisiSprat(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
