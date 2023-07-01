using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LicencaController:ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiLicencu/{idUpravnik}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLicencu([FromRoute(Name ="idUpravnik")] string id)
        {
            try
            {
                return new JsonResult(DataProvider.PreuzmiLicencu(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajLicencu/{idUpravnika}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddLicencu([FromBody] LicencaPregled l, [FromRoute(Name ="idUpravnika")] string id)
        {
            try
            {
                DataProvider.DodajLicencu(l, id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut]
        [Route("IzmeniLicencu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateLicencu([FromBody] LicencaPregled l)
        {
            try
            {
                DataProvider.IzmeniLicencu(l);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiLicencu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteLicencu([FromRoute(Name ="id")] int id)
        {
            try
            {
                DataProvider.ObrisiLicencu(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
