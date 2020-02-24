using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSharpEntityFrameworkPRSLibrary.Models {
    public class Customer {

        [Key]//if don't make it id but want it to be the PK then add [Key] not necessary
        public int Id { get; set; }//EF assumes this is the primary key or CustomerId and generates it
        [StringLength(30)] //attribute used by EF to make name only 30 chars
        [Required]  //not allowed to be null
        public string Name { get; set; }  //EF gets nvarchar(MAX) approx 2 billion add some metadata
        public double Sales { get; set; }
        public bool Active { get; set; }

        public Customer() { }//default constructor

    }
}
