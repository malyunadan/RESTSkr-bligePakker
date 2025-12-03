using Microsoft.AspNetCore.Mvc;
using SkrøblighedsPakkeLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTSkrøbligePakker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimitProfileController : ControllerBase
    {
        private readonly ILimitProfileRepository _limitProfileRepository; // ← rettet til interface
        public LimitProfileController(ILimitProfileRepository limitProfileRepository)
        {
            _limitProfileRepository = limitProfileRepository;
        }

        // GET: api/<LimitProfileController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //henter alle limit profiles fra repositoryet og returnerer dem med en 200 OK status
        public ActionResult<IEnumerable<LimitProfile>> GetAllLimitProfiles()
        {
            return Ok(_limitProfileRepository.GetAllLimitProfiles());
        }


        // GET api/<LimitProfileController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //henter en limit profile baseret på id fra repositoryet
        public ActionResult<LimitProfile> GetLimitProfileById(int id)
        {
            var limitProfile = _limitProfileRepository.GetLimitProfileById(id);// 404
            if (limitProfile == null)
            {
                return NotFound("Ingen LimitProfiles med id " + id); // 404 Not Found
            }
            return Ok(limitProfile); // 200 OK
        }

        // POST api/<LimitProfileController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //tilføjer en ny limit profile til repositoryet
        public IActionResult AddLimitProfile([FromBody] LimitProfile profile)
        {
            if (profile == null)
            {
                return BadRequest("LimitProfile objektet er null");// 400 Bad Request
            }
            _limitProfileRepository.AddLimitProfile(profile);

            return CreatedAtAction(nameof(GetLimitProfileById), new { id = profile.Id }, profile); // 201 Created
        }
    }
}
