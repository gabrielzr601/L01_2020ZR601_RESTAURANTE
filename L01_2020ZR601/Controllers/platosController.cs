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
        /// <summary>
        /// Endpoint que retorna el listado de los datos existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listadoplato = (from e in _restauranteContexto.platos
                                           select e).ToList();

            if (listadoplato.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoplato);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarplatos([FromBody] platos plato)
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
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] platos platoModificar)
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
        [Route("eliminar/{id}")]

        public IActionResult Eliminarpedido(int id)
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
        

    }
}
