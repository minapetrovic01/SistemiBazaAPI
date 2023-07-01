using DatabaseAccess;
using Microsoft.AspNetCore.Mvc;
using UpravnikProjekat;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UgovorController : ControllerBase
    {
        [HttpGet]
        [Route("PreuzmiUgovore")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUgovori()
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSveUgovore());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajUgovor/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajUgovor([FromBody] UgovorPregled ugovor, [FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                DataProvider.DodajUgovor(ugovor,id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiUgovor/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiUgovor([FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                DataProvider.ObrisiUgovor(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("IzmeniUgovor/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniUgovor([FromBody] UgovorPregled ugovor, [FromRoute(Name = "idZgrade")] int id)
        {
            try
            {
                DataProvider.IzmeniUgovor(ugovor,id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
