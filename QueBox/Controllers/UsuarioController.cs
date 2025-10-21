using QueBox.Contexts;
using QueBox.Models;
using QueBox.Query.Interfaces;
using QueBox.Repository.Interfaces;
using QueBox.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QueBox.Controllers
{
    /// <summary>
    /// Controlador para las acciones de usuarios
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly ApiContext _db;
        private readonly IUsuarioQueries _usuarioQueries;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(
            ILogger<UsuarioController> logger, ApiContext apiContext, IUsuarioQueries usuarioQueries,
            IUsuarioRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = apiContext ?? throw new ArgumentNullException(nameof(apiContext));
            _usuarioQueries = usuarioQueries ?? throw new ArgumentException(nameof(usuarioQueries));
            _logger.LogInformation("Entrando al constructor");
            _usuarioRepository = repository ?? throw new ArgumentException(nameof(repository));
        }

        /// <summary>
        /// Metodo que lista todas los usuarios
        /// </summary>
        /// <response code="200">Lista de usuarios</response>
        /// <response code="500">Error procesando la peticion</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarUsuario()
        {
            //var rs = _db.Usuarios.ToList();
            var rsDapper = await _usuarioQueries.GetAll();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar usuario por id
        /// </summary>
        /// <param name="id">Id usuario a buscar</param>
        /// <response code="200">Cuando se encuentra la usuario</response>
        /// <response code="404">la Usuario no existe</response>
        /// <response code="500">Error procesando la peticion</response>

        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarById(int id)
        {
            _logger.LogInformation("Buscando por id => {0}", id);
            Usuario? u = _db.Usuarios.FirstOrDefault(f => f.ID_Usuario == id);

            if (u == null)
            {
                _logger.LogWarning("Usuario no encontrada en bd");
                return NotFound("Coja oficio no existe");
            }

            _logger.LogInformation("Usuario encontrada. ID_Diseno=>{0}, Numero de capa => {3}", u.ID_Diseno, u.Numero);

            return Ok(u);

        }


        /// <summary>
        /// Add usuario con dapper
        /// </summary>
        /// <param name="u">Body</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Crear(Usuario u)
        {
            try
            {
                var rs = await _usuarioRepository.Add(u);
                u.Id = rs;
                return Ok(u);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}