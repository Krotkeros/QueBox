using MiApi.Contexts;
using MiApi.Models;
using MiApi.Query.Interfaces;
using MiApi.Repository.Interfaces;
using MiApi.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers
{
    /// <summary>
    /// Controlador para las acciones de personas
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IAnimal _animal;
        private readonly ILogger<PersonaController> _logger;
        private readonly ApiContext _db;
        private readonly IPersonaQueries _personaQueries;
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IAnimal animal,
            ILogger<PersonaController> logger, ApiContext apiContext, IPersonaQueries personaQueries,
            IPersonaRepository repository)
        {
            _animal = animal ?? throw new ArgumentNullException(nameof(animal));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = apiContext ?? throw new ArgumentNullException(nameof(apiContext));
            _personaQueries = personaQueries ?? throw new ArgumentException(nameof(personaQueries));
            _logger.LogInformation("Entrando al constructor");
            _personaRepository = repository ?? throw new ArgumentException(nameof(repository));
        }

        /// <summary>
        /// Metodo que lista todos los usuarios
        /// </summary>
        /// <response code="200">Lista de usuarios</response>
        /// <response code="500">Error procesando la peticion</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Persona>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarPersona()
        {
            //_animal.Morir();
            //var rs = _db.Personas.ToList();
            var rsDapper = await _personaQueries.GetAll();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar usuario por id
        /// </summary>
        /// <param name="id">Id usuario a buscar</param>
        /// <response code="200">Cuando se encuentra el usuario</response>
        /// <response code="404">El Usuario no existe</response>
        /// <response code="500">Error procesando la peticion</response>

        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(Persona), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarById(int id)
        {
            _logger.LogInformation("Buscando por id => {0}", id);
            Persona? p = _db.Personas.FirstOrDefault(f => f.Id == id);

            if (p == null)
            {
                _logger.LogWarning("Persona no encontrada en bd");
                return NotFound("Coja oficio no existe");
            }

            _logger.LogInformation("Persona encontrada. Nombre=>{0}, Edad => {1}", p.Nombre, p.Edad);

            return Ok(p);

        }


        /// <summary>
        /// Add persona con dapper
        /// </summary>
        /// <param name="p">Body</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Crear(Persona p)
        {
            try
            {
                var rs = await _personaRepository.Add(p);
                p.Id = rs;
                return Ok(p);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}