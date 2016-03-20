using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValantTest.Application.DTO;
using ValantTest.Client.Sdk.Services;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new InventoryClient();

            var result = client.GetByItemLablel("teste").Result;

            var cenas = new ItemDto { Description = "cenas", ExpirationDate = DateTime.Parse("2016 - 03 - 16T21: 50:12.609Z"), Label="cenas de teste", Type="2"  };

            var res = client.AddItem(cenas).Result;

            Console.ReadLine();
        }
    }
}
