using Microsoft.AspNetCore.Mvc;
using DatabaseAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StambenaZgradaController : ControllerBase
    {

        [HttpGet]
        [Route("PreuzmiStambeneZgrade")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStambeneZgrade()
        {
            try
            {
                return new JsonResult(DataProvider.IzlistajSveZgrade());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajStambenuZgradu/{idUpravnika}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajStambenuZgradu(string idUpravnika)
        {
            try
            {
                DataProvider.DodajZgradu(idUpravnika);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiStambenuZgradu/{idZgrade}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiStambenuZgradu([FromRoute(Name ="idZgrade")] int id)
        {
            try
            {
                DataProvider.ObrisiZgradu(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("IzmeniStambenuZgradu/{idZgrade}/{idUpravnika}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IzmeniStambenuZgradu([FromRoute(Name ="idZgrade")] int id, [FromRoute(Name ="idUpravnika")] string idUpravnika)
        {
            try
            {
                DataProvider.IzmeniUpravnikaZgrade(id, idUpravnika);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        
        
       
    }
}