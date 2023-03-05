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

        /// <summary>
        /// Endpoint que retorna el listado de los datos existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listadopedido = (from e in _restauranteContexto.pedidos
                                           select e).ToList();

            if (listadopedido.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadopedido);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarpedidos([FromBody] pedidos pedido)
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
        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
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

        public IActionResult Eliminarpedido(int id)
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

        /// <summary>
        /// EndPoint que retorna los registros de una tabla filtrados por cliente 
        /// </summay>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult BuscarPorCliente(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.clienteId ==id
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
            
        }
        /// <summary>
        /// EndPoint que retorna los registros de una tabla filtrados por motoristas 
        /// </summay>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult BuscarPorMotorista(int id)
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
