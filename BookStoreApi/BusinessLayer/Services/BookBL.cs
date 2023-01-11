﻿using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(AddBook addBook)
        {
            try
            {
                return this.bookRL.AddBook(addBook);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            return this.bookRL.GetAllBooks();
        }
        public BookModel GetBookById(int bookId)
        {
            return this.bookRL.GetBookById(bookId);
        }
        public string DeleteBook(int bookId)
        {
            return this.bookRL.DeleteBook(bookId);
        }
        public BookModel UpdateBook(BookModel updateBook)
        {
            return this.bookRL.UpdateBook(updateBook);
        }

    }
}
