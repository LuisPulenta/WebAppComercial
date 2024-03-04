using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppComercial.Api.Data;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Api.Controllers
{
    [ApiController]
    [Route("/api/saledetails")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SaleDetailsController : ControllerBase
    {
        private readonly DataContext _context;

        public SaleDetailsController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(SaleDetailDTO saledetailDTO)
        {
            if (saledetailDTO is null)
            {
                throw new ArgumentNullException(nameof(saledetailDTO));
            }

            Sale? sale = await _context.Sales!.FindAsync(saledetailDTO.SaleId);
            Product? product = await _context.Products!.FindAsync(saledetailDTO.ProductId);

            SaleDetail newSaleDetail = new SaleDetail();
            newSaleDetail.SaleId = saledetailDTO.SaleId;
            newSaleDetail.ProductId = saledetailDTO.ProductId;
            newSaleDetail.Sale = sale!;
            newSaleDetail.Product = product!;
            newSaleDetail.Price = saledetailDTO.Price;
            newSaleDetail.Name = saledetailDTO.Name;
            newSaleDetail.Quantity = saledetailDTO.Quantity;
            newSaleDetail.MoveId = saledetailDTO.MoveId;
            newSaleDetail.PercentageDiscount = saledetailDTO.PercentageDiscount;
            newSaleDetail.PercentageIVA = saledetailDTO.PercentageIVA;

            _context.Add(newSaleDetail);
            
            await _context.SaveChangesAsync();
            return Ok(newSaleDetail);            
        }
    }
}