using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImenaStanaraController : ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiImenaStanara/{idStana}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetImenaStanara([FromRoute(Name ="idStana")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajImenaStanara(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("PreuzmiImenaStanaraZgrade/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetImenaStanaraZgrade([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajImenaStanaraZgrade(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajImeStanara/{idStana}/{ime}/{prezime}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajImeStanara([FromRoute(Name ="idStana")] int id, [FromRoute(Name ="ime")] string ime, [FromRoute(Name ="prezime")] string prezime)
        {
            try
            {
                DataProvider.DodajImeStanara(id, ime, prezime);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiImeStanara/{idStanara}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiImeStanara([FromRoute(Name ="idStanara")] int id)
        {
            try
            {
                DataProvider.ObrisiImeStanara(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("IzmeniImeStanara")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniImeStanara([FromBody] ImenaStanaraPregled stanar)
        {
            try
            {
                DataProvider.IzmeniImeStanara(stanar);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
