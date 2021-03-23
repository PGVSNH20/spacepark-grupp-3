using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpacePark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SpaceshipReader spaceshipReader = new SpaceshipReader();
            PeopleReader peopleReader  = new PeopleReader();

            // Tar in ett namn från användaren
            Console.WriteLine("Name: ");
            string input = Console.ReadLine();
            input = input.Trim().ToLower();

            var list = await peopleReader.GetPeople();
            bool namePasses = false;

            // Loop för att kolla om det inmatade namnet matchar med ett namn i listan med karaktärer
            foreach (var people in list)
            {
                if (input == people.Name.ToLower())
                {
                    namePasses = true;
                    break;
                }
            }

            if (namePasses) Console.WriteLine("Input accepted");
            else Console.WriteLine("Input not accepted");

        }
    }
}
