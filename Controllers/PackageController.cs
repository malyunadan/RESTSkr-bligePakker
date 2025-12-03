using SkrøblighedsPakkeLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTSkrøbligePakker.Controllers
{
    [Route("api/[controller]")] // URL bliver "api/packages"
    [ApiController]// Gør klassen til en API-controller (automatisk modelbinding, validering osv.)
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository;

        // Her får vi repository ind via "dependency injection"
        public PackageController(IPackageRepository PackageRepository)
        {
            _packageRepository = PackageRepository;
        }
        // GET: api/<PackageController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Package>> GetAll()
        {
            //hente alle pakker fra DB via repository
            return Ok(_packageRepository.GetAllPackages());
            // 200 OK → returnér alle packages
        }

        // GET api/<PackageController>/
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
         public ActionResult<Package> GetById(int id)
        {
            // hente en pakke med det givne id fra DB via repository
            var package = _packageRepository.GetPackageById(id);
            // hvis pakken med det givne id ikke findes, returnér 404 Not Found
            if (package == null)
            {
                return NotFound("Ingen Packages med id" + id ); // 404 Not Found → hvis pakken ikke findes
            }
            return Ok(package); // 200 OK → returnér den fundne pakke
        }

        // POST api/<PackageController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPackage([FromBody] Package package)
        {
            // Tilføj en ny pakke til DB via repository
            if (package == null)
            {
                return BadRequest("Package objektet er null"); // 400 Bad Request → hvis det modtagne package objekt er null
            }
            _packageRepository.AddPackage(package);
          
            return CreatedAtAction(nameof(GetById), new { id = package.Id }, package); // 201 Created → returnér den oprettede pakke
        }
    }
}
