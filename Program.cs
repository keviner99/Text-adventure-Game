using System;
using System.IO;

// Namespace groups related code into a logical block
namespace TextAdventureGame
{
    // Define a struct named Item
    // Structs are value types and useful for small data containers
    struct Item
    {
        public string Name;
        public int Value;

        public Item(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    // Define a Player class
    class Player
    {
        // Properties store player details
        public string Name { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }

        // Constructor initializes the Player object
        public Player(string name)
        {
            Name = name;
            Health = 100;
            Gold = 0;
        }

        // Method to apply damage to the player
        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;

            Console.WriteLine($"{Name} takes {amount} damage. Health: {Health}");
        }

        // Method to collect gold
        public void CollectGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"{Name} collects {amount} gold. Total gold: {Gold}");
        }
    }

    // Program class where execution begins
    class Program
    {
        static void Main(string[] args)
        {
            // Set a custom console window title
            Console.Title = "Epic Text Adventure";

            Console.WriteLine("Welcome to the Epic Text Adventure!");
            Console.WriteLine("You are Steve, a farmer in the Middle Ages who woke up far away from home.");

            // Prompt for the player's name
            Console.Write("Enter your name: ");
            string? playerNameInput = Console.ReadLine();
            string playerName = playerNameInput ?? "Steve";

            // Create a new Player object
            Player player = new Player(playerName);

            // Create a health potion using our struct
            Item potion = new Item("Health Potion", 50);

            // Boolean variable to control the main game loop
            bool keepPlaying = true;

            // The game loop runs until the player quits or dies
            while (keepPlaying && player.Health > 0)
            {
                Console.WriteLine("\nYou are lost and need to find a refuge before the sun comes down. Where will you go?");
                Console.WriteLine("1. Enter into the dark forest.");
                Console.WriteLine("2. Climb the misty mountains.");
                Console.WriteLine("3. Visit the nearby village.");
                Console.WriteLine("4. Drink a mystery potion.");
                Console.WriteLine("5. Quit the adventure.");
                Console.Write("Your choice (1-5): ");

                string? inputRaw = Console.ReadLine();
                string input = inputRaw?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("No input detected. Please try again.");
                    continue;
                }

                // Process player input using a switch statement
                switch (input)
                {
                    case "1":
                        ForestPath(player);
                        break;
                    case "2":
                        MountainPath(player);
                        break;
                    case "3":
                        VillagePath(player);
                        break;
                    case "4":
                        DrinkPotion(player, potion);
                        break;
                    case "5":
                        keepPlaying = false;
                        Console.WriteLine("You decide to end your adventure for now.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            // Check if the player died during the game
            if (player.Health <= 0)
            {
                Console.WriteLine("\nYour wounds prove too severe. You collapse and fade into darkness.");
            }
            else
            {
                Console.WriteLine($"\nYour adventure ends here, {player.Name}!");
            }

            // Expression example: compose a summary message
            string result = $"{player.Name} ended the game with {player.Gold} gold and {player.Health} health.";
            Console.WriteLine(result);

            // Save the result to a text file
            WriteResultToFile(result);

            Console.WriteLine("\nGame result saved to 'game_results.txt'.");
            Console.ReadKey();
        }

        // Method for the forest path adventure
        static void ForestPath(Player player)
        {
            Console.WriteLine("\nYou step into the dark forest, you feel someone or something is watching you.");

            Console.WriteLine("Suddenly, a pack of wolves emerges from the bushes!");
            player.TakeDamage(25);

            if (player.Health > 0)
            {
                Console.WriteLine("You drive off the wolves and discover a hidden temple.");
                Console.WriteLine("Inside, you find gold scattered on the floor.");
                player.CollectGold(40);
            }
        }

        // Method for the mountain path adventure
        static void MountainPath(Player player)
        {
            Console.WriteLine("\nYou begin climbing the misty mountain trails.");

            Console.WriteLine("Suddenly, an avalanche roars down the slopes!");
            player.TakeDamage(35);

            if (player.Health > 0)
            {
                Console.WriteLine("You survive and stumble into a hidden cave full of gems.");
                player.CollectGold(60);
            }
        }

        // Method for the village path adventure
        static void VillagePath(Player player)
        {
            Console.WriteLine("\nYou enter the bustling village marketplace.");

            Console.WriteLine("A merchant offers you a deal: pay 20 gold for a map to hidden treasure.");
            Console.Write("Do you accept? (yes/no): ");
            string? choiceInput = Console.ReadLine();
            string choice = choiceInput?.Trim().ToLower() ?? "";

            if (choice == "yes" && player.Gold >= 20)
            {
                player.Gold -= 20;
                Console.WriteLine("You buy the map and discover a chest containing 100 gold!");
                player.CollectGold(100);
            }
            else if (choice == "yes")
            {
                Console.WriteLine("You do not have enough gold. The merchant laughs and walks away.");
            }
            else
            {
                Console.WriteLine("You ignore the merchant, but a thief tries to pick your pocket!");
                player.TakeDamage(10);
            }
        }

        // Method for drinking a potion
        static void DrinkPotion(Player player, Item potion)
        {
            if (player.Health < 100)
            {
                player.Health += potion.Value;

                // Prevent health from exceeding max value
                if (player.Health > 100)
                    player.Health = 100;

                Console.WriteLine($"{player.Name} drinks a potion. Health restored to {player.Health}.");
            }
            else
            {
                Console.WriteLine("You are already at full health.");
            }
        }

        // Method to save the result to a text file
        static void WriteResultToFile(string message)
        {
            try
            {
                File.WriteAllText("game_results.txt", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
            }
        }
    }

}
