using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Vml;
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
    [Route("/api/customers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Customers
                .Include(x => x.DocumentType!)
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
            var queryable = _context.Customers
                .Include(x => x.DocumentType!)
                .AsQueryable();
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
            var queryable = _context.Customers
                .Include(x => x.DocumentType!)
                .AsQueryable();
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
            var queryable = _context.Customers
                .Include(x => x.DocumentType!)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            return Ok(count);
        }

        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(CustomerDTO customerDTO)
        {
            try
            {
                DocumentType doc = await _context.DocumentTypes!.FindAsync(customerDTO.DocumentTypeId);

                Customer newCustomer = new()
                {
                    Id = customerDTO.Id,
                    Name = customerDTO.Name,
                    DocumentType= doc!,
                    DocumentTypeId = customerDTO.DocumentTypeId,
                    Document = customerDTO.Document,
                    ContactName = customerDTO.ContactName,
                    Address = customerDTO.Address,
                    LandPhone = customerDTO.LandPhone,
                    CellPhone = customerDTO.CellPhone,
                    Email = customerDTO.Email,
                    Remarks = customerDTO.Remarks
                };
                _context.Add(newCustomer);
                await _context.SaveChangesAsync();
                return Ok(newCustomer);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe un Cliente con el mismo nombre.");
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
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        //---------------------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> Put(CustomerDTO customerDTO)
        {
            try
            {
                Customer customer = await _context.Customers
                    .FirstOrDefaultAsync(x => x.Id == customerDTO.Id);
                if (customer == null)
                {
                    return NotFound();
                }
                customer.Address = customerDTO.Address;
                customer.Document = customerDTO.Document;
                customer.DocumentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == customerDTO.DocumentTypeId);
                customer.DocumentTypeId = customerDTO.DocumentTypeId;
                customer.CellPhone = customerDTO.CellPhone;
                customer.LandPhone = customerDTO.LandPhone;
                customer.Email = customerDTO.Email;
                customer.Remarks = customerDTO.Remarks;
                customer.Name=customerDTO.Name;

                _context.Update(customer);
                await _context.SaveChangesAsync();
                return Ok(customerDTO);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe un Cliente con el mismo nombre.");
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
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}