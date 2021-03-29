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
            var character = ValidateInput(input, list);

            if (character != null) Console.WriteLine("Input accepted");
            else Console.WriteLine("Input not accepted");

            var spaceshipList = await spaceshipReader.GetSpaceship();
            var characterShips = GetSpaceship(character, spaceshipList);

            Console.WriteLine();
            Console.WriteLine($"{character.Name}'s ships");

            foreach(var ship in characterShips)
            {
                Console.WriteLine($"{ship.Name}");
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
