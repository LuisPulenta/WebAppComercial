using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppComercial.Api.Data;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Api.Controllers
{
    [ApiController]
    [Route("/api/storeproducts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StoreproductsController : ControllerBase
    {
        private readonly DataContext _context;

        public StoreproductsController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("{productid:int}")]
        public async Task<IActionResult> GetAsync(int productid)
        {
            var queryable = _context.Storeproducts
                .Include(x => x.Store!)
                .AsQueryable();
            queryable = queryable.Where(x => x.ProductId == productid);
            return Ok(await queryable
                .OrderBy(x => x.StoreId)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("{productid:int}/{storeid:int}")]
        public async Task<IActionResult> GetByProductAndStoreAsync(int productid,int storeid)
        {
            var queryable = _context.Storeproducts
                .Include(x => x.Store!)
                .Include(x => x.Product!)
                .AsQueryable();
            queryable = queryable.Where(x => x.ProductId == productid && x.StoreId == storeid);
            return Ok(await queryable
                .OrderBy(x => x.StoreId)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(StoreproductDTO storeproductDTO)
        {
            try
            {
                Product? product = await _context.Products!.FindAsync(storeproductDTO.ProductId);
                Store? store = await _context.Stores!.FindAsync(storeproductDTO.StoreId);

                Storeproduct newStoreproduct = new()
                {
                    Id = storeproductDTO.Id,
                    Maximum = storeproductDTO.Maximum,
                    Minimum = storeproductDTO.Minimum,
                    Minimumquantity = storeproductDTO.Minimumquantity,
                    Replacementdays = storeproductDTO.Replacementdays,
                    Product=product!,
                    Store=store!,
                    StoreId= storeproductDTO.StoreId,
                    Stock=0,
                    ProductId = storeproductDTO.ProductId,
                };
                _context.Add(newStoreproduct);
                await _context.SaveChangesAsync();
                return Ok(newStoreproduct);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existen parámetros de Almacén para este Almacén.");
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
    }
}