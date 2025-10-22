using QueBox.Context;
using QueBox.Query.Models;
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

        public DisenoController(
            ILogger<DisenoController> logger,
            ApiContext apicontext,
            IDisenoQueries disenoQueries,
            IDisenoRepository disenoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = apicontext ?? throw new ArgumentNullException(nameof(apicontext));
            _disenoQueries = disenoQueries ?? throw new ArgumentNullException(nameof(disenoQueries));
            _disenoRepository = disenoRepository ?? throw new ArgumentNullException(nameof(disenoRepository));
            _logger.LogInformation("Entrando al constructor de DisenoController");
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
            var rsDapper = await _disenoQueries.ObtenerTodosAsync();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar diseño por id
        /// </summary>
        /// <param name="id">Id del diseño a buscar</param>
        /// <response code="200">Cuando se encuentra el diseño</response>
        /// <response code="404">El diseño no existe</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(Diseno), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarById(int id)
        {
            _logger.LogInformation("Buscando diseño por id -> {0}", id);
            Diseno? diseno = _db.Disenos.FirstOrDefault(f => f.Id_Diseno == id);

            if (diseno == null)
            {
                _logger.LogWarning("Diseño no encontrado en la base de datos");
                return NotFound("El diseño no existe");
            }

            _logger.LogInformation("Diseño encontrado. id usuario ->{0}, Largo -> {1}, Alto -> {2}, Ancho -> {3},Nombre -> {4}, ",
                d.Id_Usuario, d.Largo, d.Alto, d.Ancho, d.Nombre);

            return Ok(d);
        }

        /// <summary>
        /// Crear un nuevo diseño
        /// </summary>
        /// <param name="diseno">Datos del nuevo diseño</param>
        [HttpPost]
        public async Task<IActionResult> Crear(Diseno diseno)
        {
            try
            {
                var rs = await _disenoRepository.Add(d);
                d.Id_Diseno = rs;
                return Ok(d);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el diseño");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el diseño");
            }
        }

        /// <summary>
        /// Actualizar un diseño existente
        /// </summary>
        /// <param name="diseno">Diseño con los datos actualizados</param>
        [HttpPut]
        public async Task<IActionResult> Actualizar(Diseno diseno)
        {
            try
            {
                await _disenoRepository.Update(diseno);
                return Ok(diseno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el diseño");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el diseño");
            }
        }

        /// <summary>
        /// Eliminar un diseño por ID
        /// </summary>
        /// <param name="id">Id del diseño a eliminar</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _disenoRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el diseño");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el diseño");
            }
        }
    }
}
