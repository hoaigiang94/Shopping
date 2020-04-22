using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping
{
    class Program
    {
        static public List<Product> Cart = new List<Product>();
        static public string[] Instruction = new string[] {
                "Enter your choice:",
                "1. Add product to cart",
                "2. Remove product from cart",
                "3. Checkout",
                "4. Sort by price",
                "5. Exit"
            };
        static public string[] ExecutableOptions = Array.ConvertAll(Enumerable.Range(1, 4).ToArray(), x => x.ToString());
        static void ShowInstruction()
        {            
            Console.WriteLine(string.Join("\n", Instruction));
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddToCart();
                    break;
                case "2":
                    RemoveFromCart();
                    break;
                case "3":
                    Checkout();
                    break;
                case "4":
                    SortProductsByPrice();
                    break;
                case "5":
                default:
                    return;
            }

            if (Array.Exists(ExecutableOptions, x => x == input))
            {
                ShowInstruction();
            }
        }

        static void AddToCart()
        {
            Console.WriteLine("Enter product name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter product price: ");
            var price = Convert.ToDouble(Console.ReadLine());
            var product = new Product(Cart.Count + 1, name, price);
            Cart.Add(product);
            Console.WriteLine($"> You've added product: {product}");
        }

        static void ListProductAmount()
        {
            int count = Cart.Count;
            Console.WriteLine($"> You have {count} product{(count == 1 ? '\0' : 's')} in your cart");
        }

        static void RemoveFromCart()
        {
            Console.WriteLine("Enter product ID: ");
            var productId = Convert.ToInt32(Console.ReadLine());
            var product = Cart.Find(x => x.Id == productId);

            if (product == null)
            {
                Console.WriteLine("> Product not found");
            } else
            {
                Cart.Remove(product);
                ListProductAmount();
            }
        }

        static void Checkout()
        {
            Console.WriteLine($"Your total: ${Cart.Sum(x => x.Price)}");
        }

        static void SortProductsByPrice()
        {
            Console.WriteLine("Enter sort direction (asc/desc):");
            var input = Console.ReadLine().ToLower();
            var sortedList = (dynamic)null;
            if (input != "asc" && input != "desc")
            {
                Console.WriteLine("Direction not supported");
                return;
            } else if (input == "asc")
            {
                sortedList = Cart.OrderBy(x => x.Price);
            } else
            {
                sortedList = Cart.OrderByDescending(x => x.Price);
            }
            foreach (Product item in sortedList)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Console Shop.");
            ShowInstruction();
        }
    }
}
