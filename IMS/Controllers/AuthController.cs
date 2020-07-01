using IMS.JWTAuth.Interfaces;
using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace IMS.Controllers
{
    [Authorize]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IJWTAuthManager _jWTAuthManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IPersonRepository personRepository, IJWTAuthManager jWTAuthManager,ILogger<AuthController> logger)
        {
            _personRepository = personRepository;
            _jWTAuthManager = jWTAuthManager;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetPerson()
        {
            
            return Ok(_personRepository.GetPersons().Select(q => new { q.Id, q.Name }).ToList());
        }


        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            
            return Ok(_personRepository.FindById(id));
        }

        [HttpPut]
        public  IActionResult PutPerson(Person person)
        {   
            try
            {
                _personRepository.Edit(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_personRepository.FindById(person.Id) ==null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(Person person)
        {
            if (!_personRepository.VerifyPerson(person))
            {
                return Unauthorized();
            }

            var token =_jWTAuthManager.Authenticate(person.Name, person.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        public void PostPerson(Person person)
        {
            _personRepository.Add(person);

        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePerson(int id)
        {
            var person =  _personRepository.FindById(id);
            if (person == null)
            {
                return NotFound();
            }
            if (person.Name == "admin")
                return BadRequest();

            _personRepository.Remove(id);

            return person;
        }

    }
}
