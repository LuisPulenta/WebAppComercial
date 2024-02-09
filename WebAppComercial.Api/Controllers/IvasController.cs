using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppComercial.Api.Data;
using WebAppComercial.Api.Helpers;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Api.Controllers
{
    [ApiController]
    [Route("/api/ivas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IvasController : ControllerBase
    {
        private readonly DataContext _context;

        public IvasController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Ivas
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Ivas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
                        
            return Ok(await queryable
                .OrderBy(x => x.Name)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Ivas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("totalRegisters")]
        public async Task<ActionResult> GetRegisters([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Ivas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            return Ok(count);
        }

        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(Iva iva)
        {
            _context.Add(iva);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(iva);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe un Iva con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        //---------------------------------------------------------------------------------------
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var iva = await _context.Ivas.FirstOrDefaultAsync(x => x.Id == id);
            if (iva is null)
            {
                return NotFound();
            }

            return Ok(iva);
        }

        //---------------------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> Put(Iva iva)
        {
            _context.Update(iva);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(iva);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe un Iva con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //---------------------------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var iva = await _context.Ivas.FirstOrDefaultAsync(x => x.Id == id);
            if (iva == null)
            {
                return NotFound();
            }
            _context.Remove(iva);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}