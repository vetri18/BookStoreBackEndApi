using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public AddToCart AddToCart(AddToCart addCart, int userId)
        {
            return this.cartRL.AddToCart(addCart, userId);
        }
        public string RemoveFromCart(int cartId)
        {
            return this.cartRL.RemoveFromCart(cartId);
        }
        public List<CartResponse> GetAllCart(int userId)
        {
            return this.cartRL.GetAllCart(userId);
        }
        public string UpdateQtyInCart(int cartId, int bookQty, int userId)
        {
            return this.cartRL.UpdateQtyInCart(cartId, bookQty, userId);
        }


    }
}
