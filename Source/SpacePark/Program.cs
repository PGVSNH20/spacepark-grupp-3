using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


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
            var character = ValidateInput(input, list);

            if (character != null) Console.WriteLine("Input accepted");
            else Console.WriteLine("Input not accepted");

            var spaceshipList = await spaceshipReader.GetSpaceship();
            var characterShips = GetSpaceship(character, spaceshipList);

            Console.WriteLine();
            Console.WriteLine($"Choose which spaceship you want to park: ");

            for(int i = 0; i < characterShips.Count; i++)
            {
                Console.WriteLine($"{i}) {characterShips[i].Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Input: ");
            var shipInput = Console.ReadLine();

            var chosenShip = -1;
            for (int i = 0; i < characterShips.Count; i++)
            {
                if (shipInput == i.ToString()) chosenShip = i; 
            }
            if (chosenShip == -1) throw new Exception("Invalid ship input");

            using (var db = new SpaceParkContext())
            {
                // Create and save a new Blog
                Console.WriteLine("Input being saved to database");
                var person = new People { Name = character.Name };
                db.People.Add(person);
                var ship = new Spaceship { Name = characterShips[chosenShip].Name };
                db.Spaceship.Add(ship);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.People
                            orderby b.Name
                            select b;

                Console.WriteLine("All people in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

            //using (var db = new BloggingContext())
            //{
            //    // Note: This sample requires the database to be created before running.

            //    // Create
            //    Console.WriteLine("Inserting a new blog");
            //    db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            //    db.SaveChanges();

            //    // Read
            //    Console.WriteLine("Querying for a blog");
            //    var blog = db.Blogs
            //        .OrderBy(b => b.BlogId)
            //        .First();

            //    // Update
            //    Console.WriteLine("Updating the blog and adding a post");
            //    blog.Url = "https://devblogs.microsoft.com/dotnet";
            //    blog.Posts.Add(
            //        new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
            //    db.SaveChanges();

            //    // Delete
            //    Console.WriteLine("Delete the blog");
            //    db.Remove(blog);
            //    db.SaveChanges();
            //}

        }

        static People ValidateInput(string input, List<People> list)
        {

            // Loop för att kolla om det inmatade namnet matchar med ett namn i listan med karaktärer
            foreach (var people in list)
            {
                if (input == people.Name.ToLower())
                {
                    return people;
                }
            }

            return null;
        }

        static List<Spaceship> GetSpaceship(People character, List<Spaceship> allSpaceships)
        {
            
            
            var listOfCharacterSpaceships = new List<Spaceship>();

            foreach(var characterSpaceship in character.Starships)
            {
                foreach (var spaceship in allSpaceships)
                {
                    if (characterSpaceship == spaceship.Url)
                    {
                        listOfCharacterSpaceships.Add(spaceship);
                        break;
                    }
                }
            }

            return listOfCharacterSpaceships;
        }
    }
}
