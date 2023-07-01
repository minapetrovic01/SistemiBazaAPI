using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;
using DatabaseAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeretniLiftController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiTeretneLiftove/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTeretneLiftove([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajTeretneLiftove(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiTeretniLift/{serijskiBroj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiTeretniLift([FromRoute(Name = "serijskiBroj")] string serijskiBroj)
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
        [Route("IzmeniTeretniLift")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniTeretniLift([FromBody] LiftTeretniPregled lift)
        {
            try
            {
                DataProvider.IzmeniTeretniLift(lift);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajTeretniLift/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajTeretniLift([FromBody] LiftTeretniPregled lift, [FromRoute(Name = "idZgrade")]int idZgrade)
        {
            try
            {
                DataProvider.DodajTeretniLift(lift,idZgrade);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
