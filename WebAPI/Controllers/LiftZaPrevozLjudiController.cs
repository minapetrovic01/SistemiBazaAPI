using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiftZaPrevozLjudiController : ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiLiftoveZaPrevozLjudi/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLiftoveZaPrevozLjudi([FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajLiftoveZaPrevozLjudi(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiLiftZaPrevozLjudi/{serijskiBroj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiLiftZaPrevozLjudi([FromRoute(Name = "serijskiBroj")] string serijskiBroj)
        {
            try
            {
                DataProvider.ObrisiLift(serijskiBroj);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("IzmeniLiftZaPrevozLjudi")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniLiftZaPrevozLjudi([FromBody] LiftZaPrevozLjudiPregled lift)
        {
            try
            {
                DataProvider.IzmeniLiftZaPrevozLjudi(lift);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajLiftZaPrevozLjudi/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajLiftZaPrevozLjudi([FromBody] LiftZaPrevozLjudiPregled lift, [FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                DataProvider.DodajLiftZaPrevozLjudi(lift, id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        
    }
}
