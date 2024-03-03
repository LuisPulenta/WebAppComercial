using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppComercial.Api.Data;
using WebAppComercial.Shared.DTOs;
using WebAppComercial.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebAppComercial.Api.Controllers
{
    [ApiController]
    [Route("/api/moves")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MovesController : ControllerBase
    {
        private readonly DataContext _context;

        public MovesController(DataContext context)
        {
            _context = context;
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("lastmove/{storeid:int}/{productid:int}")]
        public async Task<IActionResult> GetLastMoveAsync(int storeid, int productid)
        {
            List<Move> moves = await _context.Moves
                .Where(x => x.StoreId == storeid && x.ProductId == productid)
                .ToListAsync();

            Move lastmove = moves.OrderByDescending(x => x.Date).FirstOrDefault()!;

            if (lastmove ==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lastmove);
            }            
        }

        //---------------------------------------------------------------------------------------
        [HttpGet("getmoves/{storeid:int}/{productid:int}")]
        public async Task<IActionResult> GetAsync(int storeid, int productid)
        {
            List<Move> moves = await _context.Moves
                .Where(x => x.StoreId == storeid && x.ProductId == productid)
                .OrderBy(x=>x.Date)
                .ToListAsync();

            return Ok(moves);
        }

        //---------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostAsync(MoveDTO moveDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Product? product = await _context.Products!.FindAsync(moveDTO.ProductId);
            Store? store = await _context.Stores!.FindAsync(moveDTO.StoreId);

            Move newMove = new()
            {
                Store = store!,
                StoreId = moveDTO.StoreId,
                Product = product!,
                ProductId = moveDTO.ProductId,
                AverageCost = moveDTO.AverageCost,
                Balance = moveDTO.Balance,
                Date = moveDTO.Date,
                Document = moveDTO.Document,
                Entrance = moveDTO.Entrance,
                Exit = moveDTO.Exit,
                LastCost = moveDTO.LastCost,
            };
            _context.Add(newMove);
            await _context.SaveChangesAsync();
            return Ok(newMove);
                 }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
      }
}