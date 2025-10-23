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
            Capa? c = _db.Capas.FirstOrDefault(f => f.Id_Capa == id);

            if (c == null)
            {
                _logger.LogWarning("Capa no encontrada en base de datos");
                return NotFound("La capa no existe");
            }

            _logger.LogInformation("Capa encontrada. Id diseno->{0}, Numero -> {1}", c.Id_Diseno, c.Numero);

            return Ok(c);
        }

        /// <summary>
        /// Agregar una nueva capa
        /// </summary>
        /// <param name="Capa">Objeto capa recibido en el body</param>
        [HttpPost]
        public async Task<IActionResult> Add(Capa c)
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
        /// Borrar capa
        /// </summary>
        /// <param name="Id_Capa"></param>
        /// <returns></returns>
        [HttpDelete("{Id_Capa}")]
        public async Task<IActionResult> Delete(int Id_Capa)
        {
            try
            {
                bool rs = await _capaRepository.Delete(Id_Capa);
                if (rs)
                    return NoContent();
                else
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Actualizar capa
        /// </summary>
        /// <param name="c"></param>
        /// <param name="Id_Capa"></param>
        /// <returns></returns>
        [HttpPut("{Id_Capa}")]
        public async Task<IActionResult> Update([FromBody] Capa c, [FromRoute] int Id_Capa)
        {
            try
            {
                if (Id_Capa == c.Id_Capa)
                {
                    bool rs = await _capaRepository.Update(c);
                    if (rs)
                        return Ok(c);
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
