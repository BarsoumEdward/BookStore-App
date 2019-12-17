using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
   public class Book
    {
        [Key]
        public int ISBN { set; get; }

        public string Title { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public string Specialization { set; get; }
        public byte[] ImageData { set; get; }
        public string ImageMimeType { set; get; }

    }
}
