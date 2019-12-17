using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
  public class Cart
    {
        private List<CartLine> linecollection = new List<CartLine>();
        public void AddItem(Book book,int quantity=1)
        {
            CartLine line = linecollection
                       .Where(b => b.Book.ISBN == book.ISBN)
                       .FirstOrDefault();

            if (line == null)
            {
                linecollection.Add(new CartLine { Book = book, Quantity = quantity });
            }
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Book book)
        {
            linecollection.RemoveAll(b=>b.Book.ISBN==book.ISBN);
        }

        public decimal ComputeTotalValue()
        {
            return linecollection.Sum(b=>b.Book.Price*b.Quantity);
        }

        public void Clear()
        {
            linecollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return linecollection; }
        }
    }

   public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
