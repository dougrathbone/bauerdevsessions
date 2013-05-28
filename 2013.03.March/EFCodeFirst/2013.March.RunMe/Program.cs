using System;
using _2013.March.Data;
using _2013.March.Domain;

namespace _2013.March.RunMe
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyDataContext())
            {

                var cat = new Category {Name = "Gifts"};
                for (var x = 0; x < 10; x++)
                {
                    Console.WriteLine("Creating 'Product {0}'", x);
                    
                    var prod = new Product
                        {
                            Name = "Product " + x,
                            Price = x * 100,
                            Category = cat  // will be created as a child record
                                            // in the same command. Scary but powerful.
                        };

                    context.Products.Add(prod);
                    context.SaveChanges();
                }
            }
            Console.ReadLine();
        }
    }
}
