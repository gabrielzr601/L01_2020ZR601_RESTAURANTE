using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020ZR601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020ZR601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristasController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public motoristasController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAllMotoristas")]
        public IActionResult GetMotoristas()
        {
            List<motoristas> listadomoto = (from e in _restauranteContexto.motoristas
                                            select e).ToList();
            if (listadomoto.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadomoto);
        }
        [HttpPost]
        [Route("AddMotoristas")]

        public IActionResult GuardarMotoristas([FromBody] motoristas motorista)
        {
            try
            {
                _restauranteContexto.motoristas.Add(motorista);
                _restauranteContexto.SaveChanges();
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizarMotoristas/{id}")]

        public IActionResult actualizarMotoristas(int id, [FromBody] motoristas motoModificar)
        {
            motoristas? motoActual = (from e in _restauranteContexto.motoristas
                                      where e.motoristaId == id
                                      select e).FirstOrDefault();

            if (motoActual == null)
            {
                return NotFound();
            }

            motoActual.nombreMotorista = motoModificar.nombreMotorista;


            _restauranteContexto.Entry(motoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(motoModificar);
        }

        [HttpDelete]
        [Route("eliminarMotoristas/{id}")]

        public IActionResult EliminarMotoristas(int id)
        {
            motoristas? motorista = (from e in _restauranteContexto.motoristas
                                     where e.motoristaId == id
                                     select e).FirstOrDefault();

            if (motorista == null)
            {
                return NotFound();
            }

            _restauranteContexto.motoristas.Attach(motorista);
            _restauranteContexto.motoristas.Remove(motorista);
            _restauranteContexto.SaveChanges();

            return Ok(motorista);
        }

        [HttpGet]
        [Route("GetByNombre")]

        public IActionResult Get(String filtro)
        {
            List<motoristas> listadomoto = (from e in _restauranteContexto.motoristas
                                            where e.nombreMotorista.Contains(filtro)
                                            select e).ToList();

            if (listadomoto.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadomoto);
        }


    }
}
