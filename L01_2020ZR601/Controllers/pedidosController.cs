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
    }
}
