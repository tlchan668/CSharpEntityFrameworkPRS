using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSharpEntityFrameworkPRSLibrary.Models {
    public class Order {

        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Description { get; set; }
        public double Amount { get; set; }
        public int CustomerId { get; set; }
        //want customer info received with order but don't create a column
        public virtual Customer Customer { get; set; }//get customer
        

        public override string ToString() => $"{Id}/ {Description}/{Amount}/{CustomerId}";

        public virtual List<Orderline> Orderlines { get; set; } //tells ef not to create a column in table but return info of orderlines
        public Order() { }

    }
}
