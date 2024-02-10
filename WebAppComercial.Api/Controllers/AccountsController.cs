using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppComercial.Api.Data;
using WebAppComercial.Api.Helpers;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Api.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;

        public AccountsController(DataContext context,IUserHelper userHelper, IConfiguration configuration)
        {
            _context = context;
            _userHelper = userHelper;
            _configuration = configuration;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Users
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => 
                x.FirstName.ToLower().Contains(pagination.Filter.ToLower())
                || x.LastName.ToLower().Contains(pagination.Filter.ToLower())
                //|| x.Rol.ToLower().Contains(pagination.Filter.ToLower())
                );
            }

            return Ok(await queryable
                .OrderBy(x => x.UserType)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Paginate(pagination)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => 
                x.FirstName.ToLower().Contains(pagination.Filter.ToLower())
                || x.LastName.ToLower().Contains(pagination.Filter.ToLower())
                //|| x.Rol.ToLower().Contains(pagination.Filter.ToLower())
                );
            }

            return Ok(await queryable
                .OrderBy(x => x.FirstName)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
               x.FirstName.ToLower().Contains(pagination.Filter.ToLower())
               || x.LastName.ToLower().Contains(pagination.Filter.ToLower())
               //|| x.Rol.ToLower().Contains(pagination.Filter.ToLower())
               );
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("totalRegisters")]
        public async Task<ActionResult> GetRegisters([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
               x.FirstName.ToLower().Contains(pagination.Filter.ToLower())
               || x.LastName.ToLower().Contains(pagination.Filter.ToLower())
               //|| x.Rol.ToLower().Contains(pagination.Filter.ToLower())
               );
            }

            double count = await queryable.CountAsync();
            return Ok(count);
        }

        //---------------------------------------------------------------------------------------
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO model)
        {
            User user = model;
            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
                return Ok(BuildToken(user));
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        //---------------------------------------------------------------------------------------
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _userHelper.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _userHelper.GetUserAsync(model.Email);

                if (!user.Active)
                {
                    return BadRequest("Usuario no activo.");
                }
                return Ok(BuildToken(user));
            }

            return BadRequest("Email o contraseña incorrectos.");
        }

        //---------------------------------------------------------------------------------------
        private TokenDTO BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        //---------------------------------------------------------------------------------------
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(User user)
        {
            try
            {
                var currentUser = await _userHelper.GetUserAsync(user.Email!);
                if (currentUser == null)
                {
                    return NotFound();
                }

                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Active = user.Active;

                var result = await _userHelper.UpdateUserAsync(currentUser);
                if (result.Succeeded)
                {
                    if (currentUser.UserType != user.UserType)
                    {
                        await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
                        await _userHelper.RemoveUserFromRoleAsync(user, currentUser.UserType.ToString());
                    }
                    return NoContent();
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //---------------------------------------------------------------------------------------
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            User user = await _userHelper.GetUserAsync(new Guid(id));
            if (user == null)
            {
                return NotFound();
            }
            await _userHelper.DeleteUserAsync(user);
            return NoContent();
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        [Route("/api/accounts/GetUserById/{id}")]
        public async Task<User> GetUserById(string Id)
        {
            User user = await _userHelper.GetUserAsync(new Guid(Id));
            return user!;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        [Route("/api/accounts/GetUserLogged")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get()
        {
            return Ok(await _userHelper.GetUserAsync(User.Identity!.Name!));
        }

        //---------------------------------------------------------------------------------------
        [HttpPost("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }
            return NoContent();
        }

    }
}
