using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppComercial.Api.Data;
using WebAppComercial.Api.Helpers;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;
using WebAppComercial.Shared.Helpers;

namespace WebAppComercial.Api.Controllers
{
    [ApiController]
    [Route("/api/products")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFilesHelper _filesHelper;

        public ProductController(DataContext context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Products
                .Include(x => x.Category!)
                .Include(x => x.Measure!)
                .Include(x => x.Iva!)
                .Include(x => x.ProductImages)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Category.Name)
                .ThenBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Products
                .Include(x => x.Category!)
                .Include(x => x.Measure!)
                .Include(x => x.Iva!)
                .Include(x => x.ProductImages)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
                        
            return Ok(await queryable
                .OrderBy(x => x.Category.Name)
                .ThenBy(x => x.Name)
                .ToListAsync());
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Products
                .Include(x => x.Category!)
                .Include(x => x.Measure!)
                .Include(x => x.Iva!)
                .Include(x => x.ProductImages)
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
            var queryable = _context.Products
                .Include(x => x.Category!)
                .Include(x => x.Measure!)
                .Include(x => x.Iva!)
                .Include(x => x.ProductImages)
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
        public async Task<ActionResult> PostAsync(ProductDTO productDTO)
        {
            try
            {
                Category? cat = await _context.Categories!.FindAsync(productDTO.CategoryId);
                Measure? mea = await _context.Measures!.FindAsync(productDTO.MeasureId);
                Iva? iva = await _context.Ivas!.FindAsync(productDTO.IvaId);

                Product newProduct = new()
                {
                    Id = productDTO.Id,
                    Name = productDTO.Name,
                    Category = cat!,
                    CategoryId = productDTO.CategoryId,
                    Iva = iva!,
                    Price = (decimal)productDTO.Price!,
                    Remarks = productDTO.Remarks,
                    Image = productDTO.Image,
                    MeasureId = productDTO.MeasureId,
                    Measure = mea!,
                    Quantity = (decimal)productDTO.Quantity!,
                    ProductImages = new List<ProductImage>()
                };
                    
                    //foreach (var productImage in productDTO.ProductImages!)
                    //    {
                    //        var photoProduct = Convert.FromBase64String(productImage);
                    //        newProduct.ProductImages.Add(new ProductImage { Image = await  n.SaveFileAsync(photoProduct, ".jpg", "products") });
                    //    }

                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return Ok(productDTO);
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
            var customer = await _context.Products
                .Include(x => x.Category!)
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        //---------------------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> Put(ProductDTO productDTO)
        {
            try
            {
                Product? product = await _context.Products
                        .Include(x => x.Category)
                .Include(x => x.ProductImages)
                    .FirstOrDefaultAsync(x => x.Id == productDTO.Id);

                if (product == null)
                {
                    return NotFound();
                }

                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == productDTO.CategoryId);
                var iva = await _context.Ivas.FirstOrDefaultAsync(x => x.Id == productDTO.IvaId);
                var measure = await _context.Measures.FirstOrDefaultAsync(x => x.Id == productDTO.MeasureId);

                product.Name = productDTO.Name;
                product.Iva = iva!;
                product.IvaId = productDTO.IvaId;
                product.Quantity = (decimal)productDTO.Quantity!;
                product.Price = (decimal)productDTO.Price!;
                product.Remarks = productDTO.Remarks;
                product.Category = category!;
                product.CategoryId = productDTO.CategoryId;
                product.Image = productDTO.Image;
                product.Measure = measure!;
                product.MeasureId = productDTO.MeasureId;
                
                _context.Update(product);
                await _context.SaveChangesAsync();
                return Ok(productDTO);
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