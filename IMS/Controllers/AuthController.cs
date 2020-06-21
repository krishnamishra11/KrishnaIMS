using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using IMS.JWTAuth.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace IMS.Controllers
{   
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMSContext _context;
        private readonly IJWTAuthManager _jWTAuthManager;

        public AuthController(IMSContext context, IJWTAuthManager jWTAuthManager)
        {
            _context = context;
            _jWTAuthManager = jWTAuthManager;
        }


        [HttpGet]
        public IActionResult GetPerson()
        {
            return Ok( _context.Person.Select(q => new {q.Id, q.Name}).ToList());
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            person.Password = "";

            return person;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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
            if (!_context.Person.Any(u => u.Name == person.Name && u.Password == person.Password))
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
            _context.Person.Add(person);
            _context.SaveChanges();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            if (person.Name == "admin")
                return BadRequest();

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
