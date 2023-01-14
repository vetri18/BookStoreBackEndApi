﻿


using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;


        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost("Add")]


        
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

        public IActionResult AddBook(AddBook addBook)
        {
            try
            {
                var res = bookBL.AddBook(addBook);
                if (res != null)
                {
                    return Created("", new { success = true, message = "Book Added sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllBook")]
        public IActionResult GetAllBook()
        {
            try
            {
                var res = bookBL.GetAllBooks();
                if (res != null)
                {
                    return Created("", new { data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to getall Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetBookById")]
        public IActionResult GetBookbyId(int BookId)
        {
            try
            {
                var res = bookBL.GetBookById(BookId);
                if (res != null)
                {
                    return Created("", new { data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to get Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
       
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBookbyId(int BookId)
        {
            try
            {
                var res = bookBL.DeleteBook(BookId);
                if (res != null)
                {
                    return Created("", new { success = true, message = "Book Deleted sucessfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to delete Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        
        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(BookModel bookModel)
        {
            try
            {
                var res = bookBL.UpdateBook(bookModel);
                if (res != null)
                {
                    return Created("", new { success = true, message = "Book updated sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to update Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

    }
}
