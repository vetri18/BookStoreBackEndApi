using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [HttpPost("Add")]
        public IActionResult AddToWishlist(AddToCart addToCart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.AddToCart(addToCart, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added to cart", data = result });

                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete("Delete")]
        public IActionResult RemoveFromlist(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.RemoveFromCart(cartId);
                if (result != null)
                {
                    return this.Ok(new { success = true });

                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet("GetAllCartlist")]
        public IActionResult GetCartlistitem()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.GetAllCart(userId);
                if (result != null)
                {
                    return this.Ok(new { data = result });

                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPut("UpdateQty")]
        public IActionResult UpdateQtyInCart(int cartId, int bookQty)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = cartBL.UpdateQtyInCart(cartId, bookQty, userId);
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = "Update Qty sucessfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to update Qty" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
