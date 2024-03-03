using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppComercial.Api.Data;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Api.Controllers
{
    [ApiController]
    [Route("/api/buydetails")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BuyDetailsController : ControllerBase
    {
        private readonly DataContext _context;

        public BuyDetailsController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(BuyDetailDTO buydetailDTO)
        {
            if (buydetailDTO is null)
            {
                throw new ArgumentNullException(nameof(buydetailDTO));
            }

            Buy? buy = await _context.Buys!.FindAsync(buydetailDTO.BuyId);
            Product? product = await _context.Products!.FindAsync(buydetailDTO.ProductId);

            BuyDetail newBuyDetail = new BuyDetail();
            newBuyDetail.BuyId = buydetailDTO.BuyId;
            newBuyDetail.ProductId = buydetailDTO.ProductId;
            newBuyDetail.Buy = buy!;
            newBuyDetail.Product = product!;
            newBuyDetail.Cost = buydetailDTO.Cost;
            newBuyDetail.Name = buydetailDTO.Name;
            newBuyDetail.Quantity = buydetailDTO.Quantity;
            newBuyDetail.MoveId = buydetailDTO.MoveId;
            newBuyDetail.PercentageDiscount = buydetailDTO.PercentageDiscount;
            newBuyDetail.PercentageIVA = buydetailDTO.PercentageIVA;

            _context.Add(newBuyDetail);
            
            await _context.SaveChangesAsync();
            return Ok(newBuyDetail);            
        }
    }
}