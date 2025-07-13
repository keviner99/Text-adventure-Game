// Import the System namespace to access basic .NET features like Console.
using System;
using System.IO;

// Define a namespace to group related classes and structures.
namespace TextAdventureGame
{
    // Define a struct named Item to represent simple objects
    // Structs are value types and store data compactly.
    struct Item
    {
        // Declare fields to store the name and value of the item.
        public string Name;
        public int Value;

        // Constructor to quickly initialize a new Item struct.
        public Item(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    // Define a class named Player to represent the game's main character (Steve).
    class Player
    {
        // Property to store the player's name.
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties
        public string Name { get; set; }
        // Property to store the player's current health points.
        public int Health { get; set; }
        // Property to store the amount of gold the player has collected.
        public int Gold { get; set; }

        // Constructor to create a new Player and initialize default values.
        public Player(string name)
        {
            Name = name;
            Health = 100; // Player starts with full health.
            Gold = 0; // Player starts with no gold.
        }

        // Method to reduce the player's health by a specific damage amount.
        public void TakeDamage(int amount)
        {
            Health -= amount;
            // Ensure that health cannot drop below zero.
            if (Health < 0) Health = 0;

            // Display updated health after taking damage.
            Console.WriteLine($"{Name} takes {amount} damage. Health: {Health}");
        }

        // Method to increase the player's gold by a specific amount.
        public void CollectGold(int amount)
        {
            Gold += amount;

            // Display updated gold amount after collecting.
            Console.WriteLine($"{Name} collects {amount} gold. Total gold: {Gold}");
        }
    }

    // Program class where execution begins
    class Program
    {
        // The entry point of the program. Main is always where execution starts.
        static void Main(string[] args)
        {
            // Set the title of the console window.
            Console.Title = "Epic Text Adventure";

            // Print introductory text for the game.
            Console.WriteLine("Welcome to the Epic Text Adventure!");
            Console.WriteLine("You are Steve, a farmer in the Middle Ages who woke up far away from home.");

            // Prompt the player to enter their name.
            Console.Write("Enter your name: ");

            // Read user input from the console, which could be null.
            string? playerNameInput = Console.ReadLine();

            // If the player didn't enter a name, default to "Steve".
            string playerName = playerNameInput ?? "Steve";

            // Create a new Player object with the provided name.
            Player player = new Player(playerName);

            // Create an Item struct to represent a health potion.
            Item potion = new Item("Health Potion", 50);

            // Boolean flag to control whether the game continues running.
            bool keepPlaying = true;

            // Main game loop. Runs until player quits or dies.
            while (keepPlaying && player.Health > 0)
            {
                // Display the main menu of choices to the player.
                Console.WriteLine("\nYou are lost and need to find a refuge before the sun comes down. Where will you go?");
                Console.WriteLine("1. Enter into the dark forest.");
                Console.WriteLine("2. Climb the misty mountains.");
                Console.WriteLine("3. Visit the nearby village.");
                Console.WriteLine("4. Drink a mystery potion.");
                Console.WriteLine("5. Quit the adventure.");
                Console.Write("Your choice (1-5): ");

                // Read the player's choice as a string.
                string? inputRaw = Console.ReadLine();

                // Trim any whitespace and handle possible nulls.
                string input = inputRaw?.Trim() ?? "";

                // If the user entered nothing, prompt again.
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("No input detected. Please try again.");
                    continue;
                }

                // Use a switch statement to handle the chosen option.
                switch (input)
                {
                    case "1":
                        // Player chose the forest path.
                        ForestPath(player);
                        break;
                    case "2":
                        // Player chose the mountain path.
                        MountainPath(player);
                        break;
                    case "3":
                        // Player chose to visit the village.
                        VillagePath(player);
                        break;
                    case "4":
                        // Player chose to drink the potion.
                        DrinkPotion(player, potion);
                        break;
                    case "5":
                        // Player chose to quit the game.
                        keepPlaying = false;
                        Console.WriteLine("You have been arrested by local soldiers. Game over!");
                        break;
                    default:
                        // Handle invalid inputs.
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            // Check if the player died or survived the adventure.
            if (player.Health <= 0)
            {
                Console.WriteLine("\nYou receive critical damage. You collapse and fade into darkness. Game Over!");
            }
            else
            {
                Console.WriteLine($"\nYour adventure ends here, {player.Name}! Game Over!");
            }

            // Create a summary message about the player's final stats.
            string result = $"{player.Name} ended the game with {player.Gold} gold and {player.Health} health.";

            // Display the result in the console.
            Console.WriteLine(result);

            // Write the game result to a file for record-keeping.
            WriteResultToFile(result);

            // Inform the player that the result has been saved.
            Console.WriteLine("\nGame result saved to 'game_results.txt'.");
            // Wait for the player to press a key before closing the console window.
            Console.ReadKey();
        }

        // Define a method that handles the forest path events.
        static void ForestPath(Player player)
        {
            Console.WriteLine("\nYou step into the dark forest, you feel someone or something is watching you.");

            Console.WriteLine("Suddenly, a pack of wolves emerges from the bushes and attack you!");
            // Inflict damage on the player.
            player.TakeDamage(25);

            if (player.Health > 0)
            {
                Console.WriteLine("You drive off the wolves and discover an abandoned temple.");
                Console.WriteLine("Inside, you find gold scattered on the floor and a sword.");
                player.CollectGold(40);
            }
        }

        // Define a method that handles the mountain path events.
        static void MountainPath(Player player)
        {
            Console.WriteLine("\nYou begin climbing the misty mountain trails to find a cave and spend the night there.");

            Console.WriteLine("Suddenly, an avalanche roars down the slopes!");
            // Inflict damage on the player.
            player.TakeDamage(35);

            if (player.Health > 0)
            {
                Console.WriteLine("You survive and stumble into a hidden cave full of gems. You find an armor and a shield.");
                player.CollectGold(60);
            }
        }

        // Define a method that handles the village path events.
        static void VillagePath(Player player)
        {
            Console.WriteLine("\nYou enter the village marketplace and talk with people about a place where you can rest.");

            Console.WriteLine("A merchant offers you a deal: pay 20 gold for a map to hidden treasure.");
            Console.Write("Do you accept? (yes/no): ");

            // Read the player's decision.
            string? choiceInput = Console.ReadLine();
            string choice = choiceInput?.Trim().ToLower() ?? "";

            // Player accepts the merchant's offer and has enough gold.
            if (choice == "yes" && player.Gold >= 20)
            {
                player.Gold -= 20;
                Console.WriteLine("You buy the map and discover a chest containing 100 gold and a sword!");
                player.CollectGold(100);
            }
            else if (choice == "yes")
            {
                // Player wants to buy but doesn't have enough gold.
                Console.WriteLine("You do not have enough gold. The merchant laughs and walks away.");
            }
            else
            {
                // Player declines the offer and encounters a thief.
                Console.WriteLine("You ignore the merchant, but a thief tries to pick your pocket!");
                player.TakeDamage(10);
            }
        }

        // Define a method that handles drinking a potion.
        static void DrinkPotion(Player player, Item potion)
        {
            if (player.Health < 100)
            {
                // Increase the player's health by the potion's value.
                player.Health += potion.Value;

                // Cap the player's health at 100.
                if (player.Health > 100)
                    player.Health = 100;

                Console.WriteLine($"{player.Name} drinks a potion. Health restored to {player.Health}.");
            }
            else
            {
                Console.WriteLine("You are already at full health.");
            }
        }

        // Define a method to save the result string to a text file.
        static void WriteResultToFile(string message)
        {
            try
            {
                // Write the message to a file named game_results.txt.
                File.WriteAllText("game_results.txt", message);
            }
            catch (Exception ex)
            {
                // Display an error message if writing to the file fails.
                Console.WriteLine($"Error writing file: {ex.Message}");
            }
        }
    }

}
