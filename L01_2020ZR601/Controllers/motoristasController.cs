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

        /// <summary>
        /// EndPoint que retorna el listado de todos los datos existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<motoristas> listadomotorista = (from e in _restauranteContexto.motoristas
                                                 select e).ToList();

            if (listadomotorista.Count() == 0) 
            { 
                return NotFound();
            }
            return Ok(listadomotorista);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Guardar([FromBody] motoristas motorista)
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
        [Route("Actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] motoristas motoristaModificar) 
        { 
            motoristas? motoristaActual = (from e in _restauranteContexto.motoristas
                                           where e.motoristaId == id
                                           select e).FirstOrDefault();
            if (motoristaActual == null)
            {
                return NotFound();
            }

            motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;

            _restauranteContexto.Entry(motoristaActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(motoristaModificar);
        
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id) 
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



        /// <summary>
        /// EndPoint que retorna los registros de una tabla filtrados por el nombre
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult BuscarnombreMoto(String filtro) 
        { 
            motoristas? motorista = (from e in _restauranteContexto.motoristas
                                     where e.nombreMotorista.Contains(filtro)
                                     select e).FirstOrDefault();
            if (motorista == null)
            {
                return NotFound();
            }

            return Ok(motorista);
        }
        


    }
}
