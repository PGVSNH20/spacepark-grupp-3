using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark
{
    public class SpaceshipReader
    {
        public async Task<List<Spaceship>> GetSpaceship()
        {
            var pages = "";
            var nextPage = 2;
            var client = new RestClient("https://swapi.dev/api/");
            var listOfSpaceships = new List<Spaceship>();

            Console.WriteLine("Getting spaceships...");

            var runLoop = true;
            while (runLoop)
            {
                var request = new RestRequest("starships/" + pages, DataFormat.Json);
                var starshipResponse = await client.GetAsync<SpaceshipList>(request);
                if (starshipResponse.Next == null) runLoop = false;

                foreach (var p in starshipResponse.Results)
                {

                    listOfSpaceships.Add(p);
                }

                pages = "?page=";
                pages += nextPage.ToString();
                nextPage++;
            }

            Console.WriteLine("Got all spaceships");

            return listOfSpaceships;
        }
    }
}
