using QueBox.Context;
using QueBox.Models;
using QueBox.Query.Interfaces;
using QueBox.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QueBox.Controllers
{
    /// <summary>
    /// Controlador para las acciones de diseños
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DisenoController : ControllerBase
    {
        private readonly ILogger<DisenoController> _logger;
        private readonly ApiContext _db;
        private readonly IDisenoQueries _disenoQueries;
        private readonly IDisenoRepository _disenoRepository;

        public DisenoController(ILogger<DisenoController> logger, ApiContext apicontext,
            IDisenoQueries disenoQueries,
            IDisenoRepository disenoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = apicontext ?? throw new ArgumentNullException(nameof(apicontext));
            _disenoQueries = disenoQueries ?? throw new ArgumentNullException(nameof(disenoQueries));
            _disenoRepository = disenoRepository ?? throw new ArgumentNullException(nameof(disenoRepository));
            _logger.LogInformation("Entrando al constructor");
        }

        /// <summary>
        /// Método que lista todos los diseños
        /// </summary>
        /// <response code="200">Lista de diseños</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Diseno>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarDisenos()
        {
            //var rs = _db.Disenos.ToList();
            var rsDapper = await _disenoQueries.ObtenerTodosAsync();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar diseño por id
        /// </summary>
        /// <param name="id">Id diseño a buscar</param>
        /// <response code="200">Cuando se encuentra el diseño</response>
        /// <response code="404">El Diseño no existe</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(Diseno), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarById(int id)
        {
            _logger.LogInformation("Buscando por id -> {0}", id);
            Diseno? d = _db.Disenos.FirstOrDefault(f => f.ID_Diseno == id);

            if (d == null)
            {
                _logger.LogWarning("Diseño no encontrado en bd");
                return NotFound("Diseño no existe");
            }

            _logger.LogInformation("Diseño encontrado. ID->{0}, Nombre -> {1}", d.ID_Diseno, d.Nombre);

            return Ok(d);
        }

        /// <summary>
        /// Add diseño con dapper
        /// </summary>
        /// <param name="d">Body</param>
        /// <returns>returns</returns>
        [HttpPost]
        public async Task<IActionResult> Crear(Diseno d)
        {
            try
            {
                var rs = await _disenoRepository.Add(d);
                d.ID_Diseno = rs;
                return Ok(d);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualizar diseño
        /// </summary>
        /// <param name="d">Diseño a actualizar</param>
        /// <response code="200">Diseño actualizado exitosamente</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpPut]
        public async Task<IActionResult> Actualizar(Diseno d)
        {
            try
            {
                await _disenoRepository.Update(d);
                return Ok(d);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Eliminar diseño
        /// </summary>
        /// <param name="id">Id del diseño a eliminar</param>
        /// <response code="200">Diseño eliminado exitosamente</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _disenoRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}