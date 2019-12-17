using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { set; get; }
        public PageingInfo paginginfo { set; get; }
        public string CurrentSpecialization{set;get;}
    }
}