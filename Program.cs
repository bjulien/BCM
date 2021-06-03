using BCM_test.Application;
using BCM_test.Infrastructure;
using System;
using System.Threading.Tasks;

namespace BCM_test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                // Display message to user to provide parameters.
                Console.WriteLine("Veuillez saisir les paramètres suivants : from, to, format.");
                Console.Read();
            }

            AgregateReleveService test = new AgregateReleveService();
            Console.WriteLine(await test.RecupereTousLesReleves(args[0], args[1], args[2]));
        }
    }
}
