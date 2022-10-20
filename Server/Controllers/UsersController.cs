using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CartographieContext _context;
        private readonly JWTSettings _jwtSettings;
        public UsersController(CartographieContext context, IOptions<JWTSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var Users = await (from user in _context.Users
                                    orderby user.Nom ascending
                                    select new User
                                    {
                                        Id = user.Id,
                                        Nom = user.Nom,
                                        Prenom = user.Prenom,
                                        CreatedAt = user.CreatedAt,
                                        Email = user.Email,
                                        Role = user.Role,
                                        Statut = user.Statut,
                                        Telephone = user.Telephone,
                                        Password = user.Password
                                    }).ToListAsync();
            return Users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet]
        [Route("GetData")]
        [Authorize]
        public async Task<ActionResult<User>> GetData([FromQuery] string data)
        {
            return await _context.Users.Where(x => x.Email == data).FirstOrDefaultAsync();
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            User utilisateur = new User();
            string pass = SecurityClass.Encrypt(password);
             utilisateur = await _context.Users.Where(x => x.Email == email && x.Password == pass).FirstOrDefaultAsync();
            if (utilisateur != null)
            {
                utilisateur.Token = GenerateAccessToken(utilisateur.Id);
            }
            return Ok(utilisateur);
        }

        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string email, string current, string password)
        {
            User utilisateur = new User();
            utilisateur = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (utilisateur != null)
            {
                if (utilisateur.Password != SecurityClass.Encrypt(current))
                {
                    return Ok(utilisateur = null);
                }
                utilisateur.Password = SecurityClass.Encrypt(password);
                _context.Entry(utilisateur).Property(x => x.Password).IsModified = true;
            }
            else
            {
                return Ok(utilisateur);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return Ok(utilisateur);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }

            _context.Entry(User).Property(x => x.Nom).IsModified = true;
            _context.Entry(User).Property(x => x.Prenom).IsModified = true;
            _context.Entry(User).Property(x => x.Telephone).IsModified = true;
            _context.Entry(User).Property(x => x.Email).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(User);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            User.Password = SecurityClass.Encrypt(User.Password);
            User.Token = SecurityClass.generateAccessToken(20);

            var utc = DateTime.UtcNow;
            var offset = 1;
            User.CreatedAt = utc - new TimeSpan(offset * -1, 0, 0);

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = User.Id }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
