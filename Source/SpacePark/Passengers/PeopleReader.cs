using RestSharp;
using SpacePark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark
{
    public class PeopleReader
    { 
        public async Task<List<People>> GetPeople()
        {
            var pages = "";
            var nextPage = 2;
            var client = new RestClient("https://swapi.dev/api/");
            var listOfPeople = new List<People>();

            Console.WriteLine();
            Console.WriteLine("Getting people...");
            var runLoop = true;
            while (runLoop)
            {
                var request = new RestRequest("people/" + pages, DataFormat.Json);
                var peopleResponse = await client.GetAsync<PeopleList>(request);
                if (peopleResponse.Next == null) runLoop = false;

                foreach (var p in peopleResponse.Results)
                {
                    listOfPeople.Add(p);
                    Console.WriteLine(p.Name);
                }

            pages = "?page=";
            pages += nextPage.ToString();
            nextPage++;
        };

        Console.WriteLine();
        Console.WriteLine("Got all people");

        return listOfPeople;
    }

}
}
