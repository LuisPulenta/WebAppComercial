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
    [Route("/api/suppliers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SuppliersController : ControllerBase
    {
        private readonly DataContext _context;

        public SuppliersController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Suppliers
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
            var queryable = _context.Suppliers
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
            var queryable = _context.Suppliers
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
            var queryable = _context.Suppliers
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
        public async Task<ActionResult> PostAsync(SupplierDTO supplierDTO)
        {
            try
            {
                DocumentType? documentType = await _context.DocumentTypes!.FindAsync(supplierDTO.DocumentTypeId);

                Supplier newSupplier = new()
                {
                    Id = supplierDTO.Id,
                    Name = supplierDTO.Name,
                    DocumentType = documentType!,
                    DocumentTypeId = supplierDTO.DocumentTypeId,
                    Document = supplierDTO.Document,
                    ContactName = supplierDTO.ContactName,
                    Address = supplierDTO.Address,
                    LandPhone = supplierDTO.LandPhone,
                    CellPhone = supplierDTO.CellPhone,
                    Email = supplierDTO.Email,
                    Remarks = supplierDTO.Remarks
                };
                _context.Add(newSupplier);
                await _context.SaveChangesAsync();
                return Ok(newSupplier);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe un Proveedor con el mismo nombre.");
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
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier is null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        //---------------------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> Put(SupplierDTO supplierDTO)
        {
            try
            {
                Supplier? supplier = await _context.Suppliers
                    .FirstOrDefaultAsync(x => x.Id == supplierDTO.Id);

                DocumentType? documentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == supplierDTO.DocumentTypeId);

                if (supplier == null)
                {
                    return NotFound();
                }
                supplier.Address = supplierDTO.Address;
                supplier.Document = supplierDTO.Document;
                supplier.DocumentType = documentType;
                supplier.DocumentTypeId = supplierDTO.DocumentTypeId;
                supplier.CellPhone = supplierDTO.CellPhone;
                supplier.LandPhone = supplierDTO.LandPhone;
                supplier.Email = supplierDTO.Email;
                supplier.Remarks = supplierDTO.Remarks;
                supplier.Name = supplierDTO.Name;

                _context.Update(supplier);
                await _context.SaveChangesAsync();
                return Ok(supplierDTO);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe un Proveedor con el mismo nombre.");
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
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }
            _context.Remove(supplier);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //---------------------------------------------------------------------------------------
        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Suppliers
                  .OrderBy(c => c.Name)
                  .ToListAsync());
        }
    }
}