using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppComercial.Api.Data;
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
        [HttpGet]
        public async Task<IActionResult> GetAsync(int storeid, int productid)
        {
            var queryable = _context.Storeproducts
                .AsQueryable();
            queryable = queryable.Where(x => x.StoreId == storeid && x.ProductId == productid);
            return Ok(await queryable
                .OrderBy(x => x.StoreId)
                .ToListAsync());
        }


        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(Storeproduct storeproduct)
        {
            if (storeproduct is null)
            {
                throw new ArgumentNullException(nameof(storeproduct));
            }

            _context.Add(storeproduct);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(storeproduct);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Este código de barras ya existe.");
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