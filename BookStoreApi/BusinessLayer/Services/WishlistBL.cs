using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public string AddToWishList(int bookId, int userId)
        {
            return this.wishlistRL.AddToWishList(bookId, userId);
        }
        public List<WishlistResponse> GetAllWishList(int userId)
        {
            return this.wishlistRL.GetAllWishList(userId);
        }
        public string RemoveFromWishList(int wishListId)
        {
            return this.wishlistRL.RemoveFromWishList(wishListId);
        }


    }
}
