﻿using DocumentFormat.OpenXml.Presentation;
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
    [Route("/api/barcodes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BarcodesController : ControllerBase
    {
        private readonly DataContext _context;

        public BarcodesController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("{productid:int}")]
        public async Task<IActionResult> GetAsync(int productid)
        {
            var queryable = _context.Barcodes
                .AsQueryable();
                queryable = queryable.Where(x => x.ProductId==productid);
            return Ok(await queryable
                .OrderBy(x => x.Code)
                .ToListAsync());
        }

       
        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(BarcodeDTO barcode)
        {
            try
            {
                Product? product = await _context.Products!.FindAsync(barcode.ProductId);

                Barcode newBarcode = new()
                {
                    Id = barcode.Id,
                    Code=barcode.Code,
                    Product=product!,
                    ProductId=barcode.ProductId,
                };
                _context.Add(newBarcode);
                await _context.SaveChangesAsync();
                return Ok(newBarcode);
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

        //---------------------------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var barcode = await _context.Barcodes.FirstOrDefaultAsync(x => x.Code == id);
            if (barcode == null)
            {
                return NotFound();
            }
            _context.Remove(barcode);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}