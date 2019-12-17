using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Domain.Abstract;
using BookStore.WebUI.Models;
using BookStore.Domain.Entities;

namespace BookStore.WebUI.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private IBookRepository repository;
        public int pageSize = 3;
        
        public BookController(IBookRepository bookRep)
        {
            repository = bookRep;
        }

        public ViewResult List(string specialization,int pageno=1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repository.Books
                       .Where(b=>specialization==null||b.Specialization==specialization)
                       .OrderBy(b => b.ISBN)
                       .Skip((pageno - 1) * pageSize)
                       .Take(pageSize),
                paginginfo = new PageingInfo
                {
                    CurrentPage=pageno,
                    ItemPerPage=pageSize,
                    TotalItems=specialization==null? repository.Books.Count():
                    repository.Books.Where(b => b.Specialization == specialization).Count()
                },
                CurrentSpecialization=specialization
            };

            return View(model);

        }


        public FileContentResult GetImage(int ISBN)
        {
            Book book = repository.Books
                       .FirstOrDefault(b =>b.ISBN == ISBN);
            if (book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }
            else
                return null;
        }
        //public ViewResult ListAll()
        //{
        //    return View(repository.Books);
        //}
    }
}