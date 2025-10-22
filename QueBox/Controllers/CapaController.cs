using QueBox.Contexts;
using QueBox.Models;
using QueBox.Query.Interfaces;
using QueBox.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QueBox.Controllers
{
    /// <summary>
    /// Controlador para las acciones de capas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CapaController : ControllerBase
    {
        private readonly ILogger<CapaController> _logger;
        private readonly ApiContext _db;
        private readonly ICapaQueries _capaQueries;
        private readonly ICapaRepository _capaRepository;

        public CapaController(
            ILogger<CapaController> logger,
            ApiContext apicontext,
            ICapaQueries capaQueries,
            ICapaRepository capaRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = apicontext ?? throw new ArgumentNullException(nameof(apicontext));
            _capaQueries = capaQueries ?? throw new ArgumentNullException(nameof(capaQueries));
            _capaRepository = capaRepository ?? throw new ArgumentNullException(nameof(capaRepository));
            _logger.LogInformation("Entrando al constructor de CapaController");
        }

        /// <summary>
        /// Método que lista todas las capas
        /// </summary>
        /// <response code="200">Lista de capas</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Capa>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarCapas()
        {
            var rsDapper = await _capaQueries.ObtenerTodasAsync();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar capa por id
        /// </summary>
        /// <param name="id">Id de la capa a buscar</param>
        /// <response code="200">Cuando se encuentra la capa</response>
        /// <response code="404">La capa no existe</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(Capa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarById(int id)
        {
            _logger.LogInformation("Buscando capa por id -> {0}", id);
            Capa? capa = _db.Capas.FirstOrDefault(f => f.Id_Capa == id);

            if (capa == null)
            {
                _logger.LogWarning("Capa no encontrada en base de datos");
                return NotFound("La capa no existe");
            }

            _logger.LogInformation("Capa encontrada. Id diseno->{0}, Numero -> {1}", p.Id_Diseno, p.Numero);

            return Ok(p);
        }

        /// <summary>
        /// Agregar una nueva capa
        /// </summary>
        /// <param name="capa">Objeto capa recibido en el body</param>
        [HttpPost]
        public async Task<IActionResult> Crear(Capa capa)
        {
            try
            {
                var rs = await _capaRepository.Add(c);
                c.Id_Capa = rs;
                return Ok(c);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando capa");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la capa");
            }
        }

        /// <summary>
        /// Actualizar una capa existente
        /// </summary>
        /// <param name="capa">Objeto capa con los datos actualizados</param>
        [HttpPut]
        public async Task<IActionResult> Actualizar(Capa capa)
        {
            try
            {
                await _capaRepository.Update(capa);
                return Ok(capa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando capa");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la capa");
            }
        }

        /// <summary>
        /// Eliminar una capa por ID
        /// </summary>
        /// <param name="id">Id de la capa a eliminar</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _capaRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando capa");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la capa");
            }
        }
    }
}
