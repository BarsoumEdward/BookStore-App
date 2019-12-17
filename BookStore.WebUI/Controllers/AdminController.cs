using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
       
        private IBookRepository repository;
        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

      
        public ViewResult Index()
        {
            
            return View(repository.Books);
        }
        [HttpPost]
        public ViewResult Index(string searchValue)
        {
            IEnumerable<Book> books;
            books = from b in repository.Books
                    where b.Description.IndexOf(searchValue) >= 0 ||
                          b.Title.IndexOf(searchValue) >= 0 ||
                          b.Specialization.IndexOf(searchValue) >= 0
                    select b;
            return View("Index",books);
        }

        public ViewResult Edit(int isbn)
        {
            Book book = repository.Books.FirstOrDefault(b=>b.ISBN==isbn);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileWrapper image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                repository.SaveBook(book);
                TempData["message"] = book.Title + " has been Saved";
                return RedirectToAction("Index");
            }
            else
            {
                //Not Completed

                return View(book);
            }
        }



        public ActionResult Create()
        {
            return View ("Edit",new Book());
        }

        public ActionResult Delete(int ISBN)
        {
            Book deleteBook = repository.Delete(ISBN);
            if (deleteBook != null)
            {
                TempData["message"] = deleteBook.Title + "has been Deleted";
            }
            return RedirectToAction("Index");
        }
    }
}