using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSharpEntityFrameworkPRSLibrary.Models {
    public class Product {

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }  
        public double Price { get; set; }
       

        public override string ToString() => $"{Id}/{Code}/{Name}/{Price}";

        public virtual List<Orderline> Orderlines { get; set; }//put collection or orderlines reads all orderlines

        public Product() { }//default constructor

    }
}



