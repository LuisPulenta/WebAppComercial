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
    [Route("/api/sales")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalesController : ControllerBase
    {
        private readonly DataContext _context;

        public SalesController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Sales
                .Include(o=> o.Store)
                .Include(o => o.Customer)
                .Include(o => o.SaleDetails)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => 
                x.Store.Name.ToLower().Contains(pagination.Filter.ToLower())
                || x.Customer.Name.ToLower().Contains(pagination.Filter.ToLower())
                );
            }

            return Ok(await queryable
                .OrderByDescending(x => x.Date)
                .Paginate(pagination)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Sales
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .Include(o => o.SaleDetails)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
               x.Store.Name.ToLower().Contains(pagination.Filter.ToLower())
               || x.Customer.Name.ToLower().Contains(pagination.Filter.ToLower())
               );
            }

            return Ok(await queryable
               .OrderByDescending(x => x.Date)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Sales
               .Include(o => o.Store)
               .Include(o => o.Customer)
               .Include(o => o.SaleDetails)
               .AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
               x.Store.Name.ToLower().Contains(pagination.Filter.ToLower())
               || x.Customer.Name.ToLower().Contains(pagination.Filter.ToLower())
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
            var queryable = _context.Sales
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .Include(o => o.SaleDetails)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x =>
               x.Store.Name.ToLower().Contains(pagination.Filter.ToLower())
               || x.Customer.Name.ToLower().Contains(pagination.Filter.ToLower())
               );
            }

            double count = await queryable.CountAsync();
            return Ok(count);
        }

        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(SaleDTO saleDTO)
        {
            if (saleDTO is null)
            {
                throw new ArgumentNullException(nameof(saleDTO));
            }

            Store? store = await _context.Stores!.FindAsync(saleDTO.StoreId);
            Customer? customer = await _context.Customers!.FindAsync(saleDTO.CustomerId);

            Sale newSale = new Sale();
            newSale.StoreId = saleDTO.StoreId;
            newSale.CustomerId = saleDTO.CustomerId;
            newSale.Date = saleDTO.Date;
            newSale.Store = store!;
            newSale.Customer = customer!;


            _context.Add(newSale);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(newSale);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe esta Venta.");
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
            var buy = await _context.Sales.FirstOrDefaultAsync(x => x.Id == id);
            if (buy is null)
            {
                return NotFound();
            }

            return Ok(buy);
        }
    }
}