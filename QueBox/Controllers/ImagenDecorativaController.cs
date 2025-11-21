using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueBox.Contexts;
using QueBox.Models;
using QueBox.Query.Interfaces;
using QueBox.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Controllers
{
    /// <summary>
    /// Controlador para las acciones de imagenDecorativas
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class ImagenDecorativaController : ControllerBase
    {
        private readonly ILogger<ImagenDecorativaController> _logger;
        private readonly ApiContext _db;
        private readonly IImagenDecorativaQueries _imagenDecorativaQueries;
        private readonly IImagenDecorativaRepository _imagenDecorativaRepository;

        public ImagenDecorativaController(
            ILogger<ImagenDecorativaController> logger, ApiContext apiContext, IImagenDecorativaQueries imagenDecorativaQueries,
            IImagenDecorativaRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = apiContext ?? throw new ArgumentNullException(nameof(apiContext));
            _imagenDecorativaQueries = imagenDecorativaQueries ?? throw new ArgumentException(nameof(imagenDecorativaQueries));
            _logger.LogInformation("Entrando al constructor");
            _imagenDecorativaRepository = repository ?? throw new ArgumentException(nameof(repository));
        }

        /// <summary>
        /// Metodo que lista todas las imagenes decorativas o las filtra por Id_Diseno
        /// </summary>
        /// <param name="disenoId">ID opcional del diseño para filtrar las imágenes.</param>
        /// <response code="200">Lista de imagenes decorativas</response>
        /// <response code="500">Error procesando la peticion</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ImagenDecorativa>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarImagenDecorativa([FromQuery] int? disenoId)
        {
            IEnumerable<ImagenDecorativa> rsDapper;

            if (disenoId.HasValue && disenoId.Value > 0)
            {
                _logger.LogInformation("Listando imágenes decorativas filtradas por Id_Diseno: {0}", disenoId.Value);
                rsDapper = await _imagenDecorativaQueries.ObtenerPorDisenoAsync(disenoId.Value);
            }
            else
            {
                _logger.LogInformation("Listando todas las imágenes decorativas (sin filtro).");
                rsDapper = await _imagenDecorativaQueries.ObtenerTodasAsync();
            }

            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar imagenDecorativa por id
        /// </summary>
        // ... (resto de métodos BuscarById, Add, Delete, Update permanecen iguales)

        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(ImagenDecorativa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarById(int id)
        {
            _logger.LogInformation("Buscando por id => {0}", id);
            ImagenDecorativa? i = _db.ImagenDecorativas.FirstOrDefault(f => f.Id_Img == id);

            if (i == null)
            {
                _logger.LogWarning("ImagenDecorativa no encontrada en bd");
                return NotFound("Coja oficio no existe");
            }

            _logger.LogInformation("ImagenDecorativa encontrada. id_Capa=>{0}, Ancho => {1}, Alto => {2}, Url => {3}", i.Id_Capa, i.Ancho, i.Alto, i.Url);

            return Ok(i);

        }


        /// <summary>
        /// Add imagenDecorativa con dapper
        /// </summary>
        /// <param name="i">Body</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(ImagenDecorativa i)
        {
            try
            {
                var rs = await _imagenDecorativaRepository.Add(i);
                i.Id_Img = rs;
                return Ok(i);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Borrar imagend decorativa
        /// </summary>
        /// <param name="Id_Img"></param>
        /// <returns></returns>
        [HttpDelete("{Id_Img}")]
        public async Task<IActionResult> Delete(int Id_Img)
        {
            try
            {
                bool rs = await _imagenDecorativaRepository.Delete(Id_Img);
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
        /// Actualizar imagend decorativa
        /// </summary>
        /// <param name="i"></param>
        /// <param name="Id_Img"></param>
        /// <returns></returns>
        [HttpPut("{Id_Img}")]
        public async Task<IActionResult> Update([FromBody] ImagenDecorativa i, [FromRoute] int Id_Img)
        {
            try
            {
                if (Id_Img == i.Id_Img)
                {
                    bool rs = await _imagenDecorativaRepository.Update(i);
                    if (rs)
                        return Ok(i);
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