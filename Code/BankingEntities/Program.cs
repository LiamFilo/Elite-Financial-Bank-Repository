using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    public  class Program
    {
        static void Main(string[] args)
        {
            User us = new User(new Address("Kriat At8a", "Zvolon", "Isreal", 4),"piloliam41@gmail.cm"
                ,"326183688","Liam Filo","052293493");

            Console.WriteLine(us.ToString());
            Console.WriteLine(us.GetHashCode());

            string name = "liam";
            Console.WriteLine(name.GetHashCode());
        }
    }
}
