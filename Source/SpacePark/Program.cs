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
            //peopleResult = new List<People>();
            List<PeopleList> list = await peopleReader.GetPeople();


            var peopleResult = peopleReader.GetPeople();

            var spaceshipsResult = spaceshipReader.GetSpaceship();

            Console.WriteLine(peopleResult);
           
        }
    }
}
