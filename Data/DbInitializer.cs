using invoiceF.Models;
using System;
using System.Linq;

namespace invoiceF.Models
{
    public static class DbInitializer
    {
        public static void Initialize(CustomerContext context)
        {
            // context.Database.EnsureCreated();

            // Look for any customers.
            if (context.Customer.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer{LastName="Carson", FirstName="Alexander", BusinessName="Lavanderia Conchita", CustomerPhone="7868971702", CustomerEmail="ygp@fiu.edu"},
                new Customer{LastName="Alonso", FirstName="Meredith" , BusinessName="Panaderia La sucia", CustomerPhone="8006005050", CustomerEmail="malonso@lokilo.com"},
                new Customer{LastName="Epifanio", FirstName="Panfilo", BusinessName="La Guapachosa", CustomerPhone="9545672345", CustomerEmail="pepifanio@fiu.edu"},
                new Customer{LastName="Correcto", FirstName="Facundo", BusinessName="Dulceria Dona Neli", CustomerPhone="1232343456", CustomerEmail="tito@fiu.edu"},
                new Customer{LastName="Chekera", FirstName="Juan J", BusinessName="Rompetimpanos de Mayari", CustomerPhone="5673452345", CustomerEmail="cheke@fiu.edu"},
                new Customer{LastName="Puentes", FirstName="Tito", BusinessName="MTV", CustomerPhone="9008907890", CustomerEmail="tito@fiu.edu"},
                new Customer{LastName="Gates", FirstName="Bill", BusinessName="Microsoft", CustomerPhone="8001001234", CustomerEmail="billy@fiu.edu"},
                new Customer{LastName="Jobs", FirstName="Steve", BusinessName="Apple", CustomerPhone="8002342456", CustomerEmail="jobs@fiu.edu"},
                new Customer{LastName="Bezzos", FirstName="Jeff", BusinessName="Amazon", CustomerPhone="7864445678", CustomerEmail="jbezzos@fiu.edu"}
            };
            foreach (Customer s in customers)
            {
                context.Customer.Add(s);
            }
            context.SaveChanges();

            var invoices = new Invoice[]
            {
                new Invoice{CustomerID=1, ServiceName="Mantenaince101",ServiceCost=125,ServiceDescription="Check everything",InvoiceCreatedAt=DateTime.Parse("10/12/2018")},
                new Invoice{CustomerID=1, ServiceName="Door change",ServiceCost=90,ServiceDescription="Change door",InvoiceCreatedAt=DateTime.Parse("11/15/2010")},
                new Invoice{CustomerID=2, ServiceName="Run cable",ServiceCost=200,ServiceDescription="Run cable through attic ",InvoiceCreatedAt=DateTime.Parse("04/01/2009")},
                new Invoice{CustomerID=3, ServiceName="Fix leak",ServiceCost=300,ServiceDescription="Check leak in faucet",InvoiceCreatedAt=DateTime.Parse("01/10/2011")},
                new Invoice{CustomerID=2, ServiceName="Change lock",ServiceCost=50,ServiceDescription="Change lock at door",InvoiceCreatedAt=DateTime.Parse("12/13/2012")}
            };
            foreach (Invoice c in invoices)
            {
                context.Invoice.Add(c);
            }
            context.SaveChanges();

        }
    }
}