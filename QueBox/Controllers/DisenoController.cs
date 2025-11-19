using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueBox.Contexts;
using QueBox.Models;
using QueBox.Query.Interfaces;
using QueBox.Repository.Implements;
using QueBox.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Método que lista todos los diseños de un usuario específico.
        /// </summary>
        /// <param name="idUsuario">Id del usuario a buscar</param>
        /// <response code="200">Lista de diseños del usuario</response>
        /// <response code="400">ID de usuario inválido</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet("usuario/{idUsuario}")] // 🚨 Ruta configurada para el cliente Blazor
        [ProducesResponseType(typeof(List<Diseno>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarDisenosByUsuario(string idUsuario)
        {
            if (!int.TryParse(idUsuario, out int userId))
            {
                _logger.LogWarning("Intento de listar diseños con ID de usuario inválido: {0}", idUsuario);
                return BadRequest("ID de usuario inválido.");
            }

            try
            {
                var rsDapper = await _disenoQueries.ObtenerPorUsuarioAsync(userId);
                return Ok(rsDapper ?? new List<Diseno>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar diseños para el usuario ID: {0}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la petición.");
            }
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
            Diseno? d = _db.Disenos.FirstOrDefault(f => f.Id_Diseno == id);

            if (d == null)
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
        public async Task<IActionResult> Add(Diseno d)
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
        /// Borrar Diseno
        /// </summary>
        /// <param name="Id_Diseno"></param>
        /// <returns></returns>
        [HttpDelete("{Id_Diseno}")]
        public async Task<IActionResult> Delete(int Id_Diseno)
        {
            try
            {
                bool rs = await _disenoRepository.Delete(Id_Diseno);
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
        /// Actualizar diseno
        /// </summary>
        /// <param name="d"></param>
        /// <param name="Id_Diseno"></param>
        /// <returns></returns>
        [HttpPut("{Id_Diseno}")]
        public async Task<IActionResult> Update([FromBody] Diseno d, [FromRoute] int Id_Diseno)
        {
            try
            {
                if (Id_Diseno == d.Id_Diseno)
                {
                    bool rs = await _disenoRepository.Update(d);
                    if (rs)
                        return Ok(d);
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
