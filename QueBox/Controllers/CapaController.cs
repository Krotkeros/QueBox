using MiApi.Contexts;
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

        public CapaController(ILogger<CapaController> logger, ApiContext apicontext, 
            ICapaQueries capaQueries,
            ICapaRepository capaRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = apicontext ?? throw new ArgumentNullException(nameof(apicontext));
            _capaQueries = capaQueries ?? throw new ArgumentNullException(nameof(capaQueries));
            _capaRepository = capaRepository ?? throw new ArgumentNullException(nameof(capaRepository));
            _logger.LogInformation("Entrando al constructor");
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
            //var rs = _db.Capas.ToList();
            var rsDapper = await _capaQueries.ObtenerTodasAsync();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar capa por id
        /// </summary>
        /// <param name="id">Id capa a buscar</param>
        /// <response code="200">Cuando se encuentra la capa</response>
        /// <response code="404">La Capa no existe</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(Capa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarById(int id)
        {
            _logger.LogInformation("Buscando por id -> {0}", id);
            Capa? p = _db.Capas.FirstOrDefault(f => f.Id_Capa == id);

            if (p == null)
            {
                _logger.LogWarning("Capa no encontrada en bd");
                return NotFound("Capa no existe");
            }

            _logger.LogInformation("Capa encontrada. Id diseno->{0}, Numero -> {1}", p.Id_Diseno, p.Numero);

            return Ok(p);
        }

        /// <summary>
        /// Add capa con dapper
        /// </summary>
        /// <param name="p">Body</param>
        /// <returns>returns</returns>
        [HttpPost]
        public async Task<IActionResult> Crear(Capa p)
        {
            try
            {
                var rs = await _capaRepository.Add(p);
                p.Id_Capa = rs;
                return Ok(p);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualizar capa
        /// </summary>
        /// <param name="p">Capa a actualizar</param>
        /// <response code="200">Capa actualizada exitosamente</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpPut]
        public async Task<IActionResult> Actualizar(Capa p)
        {
            try
            {
                await _capaRepository.Update(p);
                return Ok(p);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Eliminar capa
        /// </summary>
        /// <param name="id">Id de la capa a eliminar</param>
        /// <response code="200">Capa eliminada exitosamente</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _capaRepository.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}