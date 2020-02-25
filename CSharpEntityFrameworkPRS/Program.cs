using CSharpEntityFrameworkPRSLibrary;
using CSharpEntityFrameworkPRSLibrary.Models;
using System;
using System.Linq;

namespace CSharpEntityFrameworkPRS {
    class Program {
        static void Main(string[] args) {
            var context = new AppDbContext();

            

            //AddProduct(context);
            //average of prod
            Console.WriteLine($"avg price {context.Products.Average(x => x.Price)}");
            //find top two rows
            var top2 = context.Products.Where(x => x.Id > 3).ToList();
            foreach(var t in top2) {
                Console.WriteLine(t);
            }
            //bring back all the customer where active is true;
            Console.WriteLine("isactive");
            var custActive = context.Customers.Where(x => x.Active).ToList();
            foreach (var t in custActive) {
                Console.WriteLine(t);
            }

            //product

            static void AddProduct(AppDbContext context) {
                var prod = new Product { Id = 0, Code = "Code1", Name = "Echo", Price = 10};
                var prod2 = new Product { Id = 0, Code = "Code2", Name = "Echo 2", Price = 20 };
                var prod3 = new Product { Id = 0, Code = "Code3", Name = "Echo 3", Price = 30};
                var prod4 = new Product { Id = 0, Code = "Code4", Name = "Echo 4", Price = 40 };
                var prod5 = new Product { Id = 0, Code = "Code5", Name = "Echo 5", Price = 50 };
                context.Products.AddRange(prod, prod2, prod3, prod4, prod5);

                var rowsAffected = context.SaveChanges();//alters db
                if (rowsAffected != 5) throw new Exception("Add Prod failed!");
                Console.WriteLine("Added 5 order");
            }

            //AddOrder(context);
            //UpdateCustomerSales(context);
            //GetAllOrders(context);

            static void UpdateCustomerSales(AppDbContext context) {
                var custOrderJoin = from c in context.Customers
                                    join o in context.Orders
                                    on c.Id equals o.CustomerId
                                    where c.Id == 2
                                    select new { Amount = o.Amount,
                                                Customer = c.Name,
                                                Order = o.Description
                                    };//brings back all the order with id 2
                var orderTotal = custOrderJoin.Sum(c => c.Amount);//get sume on custorderjoin
                var cust = context.Customers.Find(2);
                cust.Sales = orderTotal;
                context.SaveChanges();
            }

            static void AddOrder(AppDbContext context) {
                //create an instance of order class and fill with data
                var order1 = new Order {Id = 0, Description = "Order 1",Amount = 100,CustomerId = 2};
                var order2 = new Order {Id = 0, Description = "order 2", Amount = 200, CustomerId = 2 };
                var order3 = new Order { Id = 0, Description = "order 3", Amount = 300, CustomerId = 2 };
                var order4 = new Order { Id = 0, Description = "order 4", Amount = 400, CustomerId = 2 };
                var order5 = new Order { Id = 0, Description = "order 5", Amount = 500, CustomerId = 2 };
                context.Orders.AddRange(order1, order2, order3, order4, order5);
                
                var rowsAffected = context.SaveChanges();//alters db
                if (rowsAffected !=5) throw new Exception("Add failed!");
                Console.WriteLine("Added 5 order"); 
            }

            static void GetAllOrders(AppDbContext context) {
                var orders = context.Orders.ToList();//this uses linq to read data
                foreach (var o in orders) {
                    Console.WriteLine(o);//overidden the to string function in both class
                }
            }

            static void GetOrderByPk(AppDbContext context) {
                var orderPK = 1;
                var order = context.Customers.Find(orderPK);
                if (order == null) throw new Exception("Order not found");
                Console.WriteLine(order);
            }


            //AddCustomer(context);
            //GetCustomerByPk(context);
            //GetAllCustomers(context);
            //UpdateCustomer(context);
            //GetAllCustomers(context);
            //DeleteCustomer(context);
            // GetAllCustomers(context);

            //AddOrderlines(context);
            GetOrderLines(context);
            //UpdateOrderAmount(context);
            recalcOrderAmounts(context);

        }
        static void UpdateOrderAmount(AppDbContext context, int orderId) {
           //
            //read the order you want to update
            var order = context.Orders.Find(orderId);
            //calculate by going trhough
            var total = order.Orderlines.Sum(ol => ol.Quantity * ol.Product.Price);
            order.Amount = total;
            var rc = context.SaveChanges();
            //only update if change so would have to check that 
            if (rc != 1) throw new Exception("order update amt failed");
        }

        //recalc all orders by using update order amount

        static void recalcOrderAmounts (AppDbContext context) {
            var orderIds = context.Orders.Select(x => x.Id).ToArray();
            foreach(var orderId in orderIds) {
                UpdateOrderAmount(context, orderId);
                    
             }

        }
        static void GetOrderLines(AppDbContext context) {
            var orderlines = context.Orderlines.ToList();
         
           // orderlines.ForEach(line => Console.WriteLine($"{line.Quantity}/{line.Order.Description}/{line.Product.Name}"));
            

            orderlines.ForEach(line => Console.WriteLine(line));
        }
        static void AddOrderlines(AppDbContext context) {
            var order = context.Orders.SingleOrDefault(o => o.Description == "order 5");
            var product = context.Products.SingleOrDefault(p => p.Code == "Code4");
            var orderline = new Orderline {
                Id = 0, ProductId = product.Id, OrderId = order.Id, Quantity = 5
        
            };//create my orderline
            
            context.Orderlines.Add(orderline);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected != 1) throw new Exception("Orderline failed;");
        }
        //read orderline with prod name, description and price
        static void DeleteCustomer(AppDbContext context) {
            var keyToDelete = 4;
            var cust = context.Customers.Find(keyToDelete);
            if (cust == null) throw new Exception("Cust not found");
            context.Customers.Remove(cust);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected != 1) throw new Exception("Delete failed!");

        }
        static void UpdateCustomer(AppDbContext context) {
            var custPK = 3;
            //read for data see if there before change
            var cust = context.Customers.Find(custPK);
            if (cust == null) throw new Exception("Cust not found");
            cust.Name = "Target";
            var rowsAffected = context.SaveChanges();
            if (rowsAffected != 1) throw new Exception("Failed to update customer");
            Console.WriteLine("update successfull");
        }

        static void GetAllCustomers(AppDbContext context) {
            var custs = context.Customers.ToList();//this uses linq to read data
            foreach (var c in custs) {
                Console.WriteLine(c);//overidden the to string function in both class
            }
        }

        static void GetCustomerByPk(AppDbContext context) {
            var custPk = 1;
            var cust = context.Customers.Find(custPk);
            if (cust == null) throw new Exception("Customer not found");
            Console.WriteLine(cust);
        }


        static void AddCustomer(AppDbContext context) {
            //create an instance of customer class and fill with data
            var cust = new Customer {
                Id = 0,
                Name = "Amazon",
                Sales = 0,
                Active = true
            };
            context.Customers.Add(cust);//modifies collection but not the db
            var rowsAffected = context.SaveChanges();//alters db
            if (rowsAffected == 0) throw new Exception("Add failed!");
            return;
        }
    }
}
