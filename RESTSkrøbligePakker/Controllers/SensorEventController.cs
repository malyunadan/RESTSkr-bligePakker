using SkrøblighedsPakkeLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTSkrøbligePakker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorEventController : ControllerBase
    {
        private readonly ISensorEventRepository _sensorEventRepository;
        //Constructoren får et repository ind via dependency injection. Det betyder, at controlleren ikke selv laver data, men får en klasse (SensorEventRepository) som kan hente og gemme data.
        public SensorEventController(ISensorEventRepository SensorEventRepository)
        {
            _sensorEventRepository = SensorEventRepository;
        }
        // GET: api/<SensorEventController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Henter alle sensor events fra repositoryet og returnerer dem med en 200 OK status
        public ActionResult<IEnumerable<SensorEvent>> GetAllSensorEvents() 
        {
            return Ok(_sensorEventRepository.GetAllSensorEvents());//Henter alle sensor events fra repositoryet og returnerer dem med en 200 OK status
        }

        // GET: api/sensorevents/{id}/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Henter en sensor event baseret på id fra repositoryet
        public ActionResult<SensorEvent> GetEventById(int id)
        {
            var evt = _sensorEventRepository.GetEventById(id);

            if (evt == null)
            {
                return NotFound("Ingen SensorEvent med id " + id); // 404 Not Found
            }

            return Ok(evt); // 200 OK
        }

        // POST api/<SensorEventController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //Tilføjer en ny sensor event til repositoryet
        public IActionResult AddSensorEvent([FromBody] SensorEvent evt) // [FromBody] betyder, at data sendes som JSON i HTTP-requestens body og automatisk bindes til objektet

        {
            if (evt == null)
            {
                return BadRequest("SensorEvent objektet er null");// 400 Bad Request
            }
            _sensorEventRepository.AddSensorEvent(evt);
            return CreatedAtAction(nameof(GetEventById), new { id = evt.Id }, evt);// 201 Created
        }
    }
}

//En Controller er altså “broen” mellem klienten (fx Postman, browser, mobilapp) og din data (repository/database).