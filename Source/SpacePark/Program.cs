using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpacePark
{
    class Program
    {
        static void Main(string[] args)
        {
            
            GetPeople();
            Thread.Sleep(20000);

        }
        
        // Tillfällig metod för att testa att ta fram karaktärer
        public static async Task GetPeople() 
        {
            var pages = "";
            var nextPage = 2;
            var client = new RestClient("https://swapi.dev/api/");
            
            Console.WriteLine("Getting people...");
            Console.WriteLine();
            var runLoop = true;
            do 
            {
                var request = new RestRequest("people/" + pages, DataFormat.Json);
                var peopleResponse = await client.GetAsync<PeopleList>(request);
                if (peopleResponse.Next == null) runLoop = false;
                
                foreach (var p in peopleResponse.Results)
                {
                    Console.WriteLine(p.Name);
                }

                pages = "?page=";
                pages += nextPage.ToString();
                nextPage++;

            } while (runLoop);

            Console.WriteLine();
            Console.WriteLine("Got all people");
        }
    }
}
