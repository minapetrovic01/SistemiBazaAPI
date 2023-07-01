using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingMestoController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiParkingMesta/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetParkingMesta([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSvaParkingMesta(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiNerezervisanaParkingMesta/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNerezervisanaParkingMesta([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSvaNerezervisanaParkingMesta(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajParkingMesto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddParkingMesto([FromBody] ParkingMestoPregled pmb)
        {
            try
            {
                DataProvider.DodajParkingMesto(pmb);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiParkingMesto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteParkingMesto([FromRoute(Name ="id")] int id)
        {
            try
            {
                DataProvider.ObrisiParkingMesto(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("IzmeniParkingMesto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateParkingMesto([FromBody] ParkingMestoPregled pmb)
        {
            try
            {
                DataProvider.IzmeniParkingMesto(pmb);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
