using Microsoft.AspNetCore.Mvc;
using API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private static readonly List<Profile> _profiles = new()
        {
            new Profile { Id = 1, Name = "Ava Morgan", BirthDate = new DateTime(2002, 4, 12), Email = "ava.morgan@example.com" },
            new Profile { Id = 2, Name = "Noah Bennett", BirthDate = new DateTime(1997, 9, 3), Email = "noah.bennett@example.com" },
            new Profile { Id = 3, Name = "Mia Chen", BirthDate = new DateTime(2005, 2, 27), Email = "mia.chen@example.com" }
        };

        // GET: api/<ProfileController>
        [HttpGet]
        public ActionResult<List<Profile>> Get()
        {
            return Ok(_profiles);
            // return notFound();
        }

        // GET api/<ProfileController>/5
        [HttpGet("{id}", Name = "GetProfileById")]
        public ActionResult<Profile> Get(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null) { 
                return NotFound();
            }
            return Ok(profile);
        }

        // POST api/<ProfileController>
        [HttpPost]
        public ActionResult<Profile> Post([FromBody] Profile newProfile)
        {
            if (_profiles.Any(p => p.Id == newProfile.Id)) {
                return Conflict("Profile already exists.");
            }
            _profiles.Add(newProfile);
            return CreatedAtRoute("GetProfileById", new { id = newProfile.Id }, newProfile);
        }

        // PUT api/<ProfileController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Profile updatedProfile){
            
            var existingProfile = _profiles.FirstOrDefault(p => p.Id == id);
            if (existingProfile == null) {
                return NotFound("Profile not found.");
            }
            existingProfile.Name = updatedProfile.Name;
            existingProfile.BirthDate = updatedProfile.BirthDate;
            existingProfile.Email = updatedProfile.Email;
            return NoContent();
        }

        // DELETE api/<ProfileController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null) {
                return NotFound( new { message = "Couldn't find profile" });
            }
            _profiles.Remove(profile);
            return NoContent();
        }
    }
}
