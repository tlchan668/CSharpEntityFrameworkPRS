using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CSharpEntityFrameworkPRSLibrary.Models {
    public class Orderline {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public override string ToString() => $"olId {Id}/PId {ProductId}/ Oid{OrderId}/ Quantity{Quantity}: order id {Order.Id}/ prod {Product.Name}";

        [JsonIgnore]//don't get the order when get the orderline
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public Orderline() {  }
    }
}
