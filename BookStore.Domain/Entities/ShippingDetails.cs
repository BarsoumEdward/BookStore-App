using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
   public class ShippingDetails
    {
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter The First Line")]
        [Display(Name="Address Line1")]
        public string Line1 { get; set; }
        [Display(Name = "Address Line2")]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Please Enter The City Name")]
        public string City { get; set; }
        public string State { get; set; }

        [Required(ErrorMessage = "Please Enter The Country Name")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }

    }
}
