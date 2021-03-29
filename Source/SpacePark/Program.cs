using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace SpacePark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SpaceshipReader spaceshipReader = new SpaceshipReader();
            PeopleReader peopleReader = new PeopleReader();

            // Välkomnar och tar in ett namn från användaren
            Console.WriteLine("Welcome to Space Park!");
            Console.WriteLine();
            
            People character = new People();
            var runNameCheckLoop = true;
            while (runNameCheckLoop)
            {
                Console.Write("Name: ");
                string nameInput = Console.ReadLine();
                nameInput = nameInput.Trim().ToLower();

                var list = await peopleReader.GetPeople();
                character = ValidateInput(nameInput, list);

                if (character == null) Console.WriteLine("Input not accepted");
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Input accepted");
                    Console.WriteLine();
                    runNameCheckLoop = false;
                }
            }

            var spaceshipList = await spaceshipReader.GetSpaceship();
            var characterShips = GetSpaceship(character, spaceshipList);

            Console.WriteLine();
            Console.WriteLine("Do you want to enter or exit parking?");
            Console.WriteLine("1) Enter");
            Console.WriteLine("2) Exit");
            var enterOrExitInput = Convert.ToInt32(Console.ReadLine());

            if (enterOrExitInput == 1)
            {
                ParkSpaceShip(character, characterShips);
            }
            else
            {
                CheckOutSpaceship(character);
            }
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

            foreach (var characterSpaceship in character.Starships)
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

        // Metod för att parkera rymdskepp
        static void ParkSpaceShip(People character, List<Spaceship> characterShips)
        {
            Console.WriteLine();
            Console.WriteLine($"Choose which spaceship you want to park: ");

            // Skriver ut rymdskepp som hör till karaktären
            for (int i = 0; i < characterShips.Count; i++)
            {
                Console.WriteLine($"{i}) {characterShips[i].Name}");
            }

            // Tar in vilket rymdskepp användaren vill parkera
            Console.WriteLine();
            Console.Write("Input: ");
            var shipInput = Console.ReadLine();

            var chosenShip = -1;
            for (int i = 0; i < characterShips.Count; i++)
            {
                if (shipInput == i.ToString()) chosenShip = i;
            }
            if (chosenShip == -1) throw new Exception("Invalid ship input");

            using (var db = new SpaceParkContext())
            {
                Console.WriteLine("Input being saved to database");
                var parking = new Parking { Name = character.Name, Spaceship = characterShips[chosenShip].Name, ParkingStart = DateTime.Now };
                db.Parking.Add(parking);
                db.SaveChanges();
            }
        }

        // Metod för att avsluta parking
        static void CheckOutSpaceship(People character)
        {

            using (var db = new SpaceParkContext())
            {

                Console.WriteLine("Which spaceship do you want to check out?");
                var characterShips = from b in db.Parking
                                     where b.Name == character.Name && b.Payment == 0
                                     select b;
                var newList = characterShips.ToList();

                for (int i = 0; i < newList.Count; i++)
                {
                    Console.WriteLine($"{i}) {newList[i].Spaceship}");
                }

                var choice = Convert.ToInt32(Console.ReadLine());

                var shipToCheckOut = from a in db.Parking
                                     where a.Name == character.Name && a.Spaceship == newList[choice].Spaceship && newList[choice].Payment == 0
                                     select a;

                var shipToCheckOutList = shipToCheckOut.ToList();

                shipToCheckOutList[0].ParkingEnd = DateTime.Now;
                var duration = (shipToCheckOutList[0].ParkingStart - shipToCheckOutList[0].ParkingEnd).Duration().TotalSeconds;
                shipToCheckOutList[0].Payment = Math.Round((decimal)(duration * 10));
                db.SaveChanges();
                Console.WriteLine($"You parked for {duration} seconds and were billed {shipToCheckOutList[0].Payment} republic credits");
                

            }
        }
    }
}
