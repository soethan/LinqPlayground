using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest.Utils
{
    public static class Extensions
    {
        public static void PrintQuery(this IEnumerable query, string title)
        {
            Console.WriteLine($"************** {title} **************");
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}
