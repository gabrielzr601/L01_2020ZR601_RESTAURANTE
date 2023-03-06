using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020ZR601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020ZR601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public platosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("Getplatos")]
        public IActionResult Getplato()
        {
            List<platos> listadoplato = (from e in _restauranteContexto.platos
                                         select e).ToList();
            if (listadoplato.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoplato);
        }
        [HttpPost]
        [Route("AddPlato")]

        public IActionResult GuardarPlato([FromBody] platos plato)
        {
            try
            {
                _restauranteContexto.platos.Add(plato);
                _restauranteContexto.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizarPlato/{id}")]

        public IActionResult actualizarPlato(int id, [FromBody] platos platoModificar)
        {
            platos? platoActual = (from e in _restauranteContexto.platos
                                   where e.platoId == id
                                   select e).FirstOrDefault();

            if (platoActual == null)
            {
                return NotFound();
            }

            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;


            _restauranteContexto.Entry(platoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(platoModificar);
        }

        [HttpDelete]
        [Route("eliminarPlato/{id}")]

        public IActionResult EliminarPlato(int id)
        {
            platos? plato = (from e in _restauranteContexto.platos
                             where e.platoId == id
                             select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }

            _restauranteContexto.platos.Attach(plato);
            _restauranteContexto.platos.Remove(plato);
            _restauranteContexto.SaveChanges();

            return Ok(plato);
        }

        [HttpGet]
        [Route("GetByprecio")]

        public IActionResult Get(decimal precio)
        {
            List<platos> listadoplato = (from e in _restauranteContexto.platos
                                         where e.precio < precio
                                         select e).ToList();
            if (listadoplato == null)
            {
                return NotFound();
            }

            return Ok(listadoplato);
        }



    }
}
