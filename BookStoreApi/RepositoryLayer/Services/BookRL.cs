﻿using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(AddBook addBook)
        {

            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", addBook.BookName);
                    cmd.Parameters.AddWithValue("@Author", addBook.Author);
                    cmd.Parameters.AddWithValue("@BookImage", addBook.BookImage);
                    cmd.Parameters.AddWithValue("@BookDetail", addBook.BookDetail);
                    cmd.Parameters.AddWithValue("@DiscountPrice", addBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", addBook.ActualPrice);
                    cmd.Parameters.AddWithValue("@Quantity", addBook.Quantity);
                    cmd.Parameters.AddWithValue("@Rating", addBook.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", addBook.RatingCount);
                    cmd.Parameters.Add("@BookId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    int bookId = Convert.ToInt32(cmd.Parameters["@BookId"].Value.ToString());
                    con.Close();

                    if (result != 0)
                    {
                        BookModel bookModel = new BookModel
                        {
                            BookId = bookId,
                            BookName = addBook.BookName,
                            Author = addBook.Author,
                            BookImage = addBook.BookImage,
                            BookDetail = addBook.BookDetail,
                            DiscountPrice = addBook.DiscountPrice,
                            ActualPrice = addBook.ActualPrice,
                            Quantity = addBook.Quantity,
                            Rating = addBook.Rating,
                            RatingCount = addBook.RatingCount
                        };
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        public List<BookModel> GetAllBooks()
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<BookModel> bookResponse = new List<BookModel>();
                    SqlCommand cmd = new SqlCommand("spGetAllBooks", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            BookModel bookModel = new BookModel
                            {
                                BookId = Convert.ToInt32(rdr["BookId"]),
                                BookName = Convert.ToString(rdr["BookName"]),
                                Author = Convert.ToString(rdr["Author"]),
                                BookImage = Convert.ToString(rdr["BookImage"]),
                                BookDetail = Convert.ToString(rdr["BookDetail"]),
                                DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]),
                                ActualPrice = Convert.ToDouble(rdr["ActualPrice"]),
                                Quantity = Convert.ToInt32(rdr["Quantity"]),
                                Rating = Convert.ToDouble(rdr["Rating"]),
                                RatingCount = Convert.ToInt32(rdr["RatingCount"])
                            };
                            bookResponse.Add(bookModel);
                        }
                        con.Close();
                        return bookResponse;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        public BookModel GetBookById(int bookId)
        {

            try
            {
                this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
                using (con)
                {
                    BookModel bookModel = new BookModel();
                    SqlCommand cmd = new SqlCommand("spGetBookById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {

                            bookModel.BookId = Convert.ToInt32(rdr["BookId"]);
                            bookModel.BookName = Convert.ToString(rdr["BookName"]);
                            bookModel.Author = Convert.ToString(rdr["Author"]);
                            bookModel.BookImage = Convert.ToString(rdr["BookImage"]);
                            bookModel.BookDetail = Convert.ToString(rdr["BookDetail"]);
                            bookModel.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]);
                            bookModel.ActualPrice = Convert.ToDouble(rdr["ActualPrice"]);
                            bookModel.Quantity = Convert.ToInt32(rdr["Quantity"]);
                            bookModel.Rating = Convert.ToDouble(rdr["Rating"]);
                            bookModel.RatingCount = Convert.ToInt32(rdr["RatingCount"]);
                        }
                        con.Close();
                        return bookModel;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteBook(int bookId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spDeleteBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Book Deleted Successfully";
                    }
                    else
                    {
                        return "Failed to Delete the Book";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        public BookModel UpdateBook(BookModel updateBook)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", updateBook.BookId);
                    cmd.Parameters.AddWithValue("@BookName", updateBook.BookName);
                    cmd.Parameters.AddWithValue("@Author", updateBook.Author);
                    cmd.Parameters.AddWithValue("@BookImage", updateBook.BookImage);
                    cmd.Parameters.AddWithValue("@BookDetail", updateBook.BookDetail);
                    cmd.Parameters.AddWithValue("@DiscountPrice", updateBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", updateBook.ActualPrice);
                    cmd.Parameters.AddWithValue("@Quantity", updateBook.Quantity);
                    cmd.Parameters.AddWithValue("@Rating", updateBook.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", updateBook.RatingCount);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return updateBook;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
    }
}
