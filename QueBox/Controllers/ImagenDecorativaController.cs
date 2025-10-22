using QueBox.Contexts;
using QueBox.Models;
using QueBox.Query.Interfaces;
using QueBox.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// Metodo que lista todas las imagenes decorativas
        /// </summary>
        /// <response code="200">Lista de imagenes decorativas</response>
        /// <response code="500">Error procesando la peticion</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ImagenDecorativa>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarImagenDecorativa()
        {
            //var rs = _db.ImagenDecorativas.ToList();
            var rsDapper = await _imagenDecorativaQueries.GetAll();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar imagenDecorativa por id
        /// </summary>
        /// <param name="id">Id imagenDecorativa a buscar</param>
        /// <response code="200">Cuando se encuentra la imagenDecorativa</response>
        /// <response code="404">la ImagenDecorativa no existe</response>
        /// <response code="500">Error procesando la peticion</response>

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
        public async Task<IActionResult> Crear(ImagenDecorativa i)
        {
            try
            {
                var rs = await _imagenDecorativaRepository.Add(i);
                p.Id = rs;
                return Ok(i);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}