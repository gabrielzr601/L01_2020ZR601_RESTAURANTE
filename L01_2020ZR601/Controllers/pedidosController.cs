using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020ZR601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020ZR601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public pedidosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listadopedido = (from e in _restauranteContexto.pedidos
                                           select e).ToList();
            if (listadopedido.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadopedido);
        }
        [HttpPost]
        [Route("Add")]

        public IActionResult Guardar([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteContexto.pedidos.Add(pedido);
                _restauranteContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizar(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual = (from e in _restauranteContexto.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();

            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminar(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.pedidoId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteContexto.pedidos.Attach(pedido);
            _restauranteContexto.pedidos.Remove(pedido);
            _restauranteContexto.SaveChanges();

            return Ok(pedido);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.clienteId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet]
        [Route("GetMotorista/{id}")]

        public IActionResult Motorista(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }


    }
}
