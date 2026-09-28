
using SchoolDatabaseManager.Classes.Models;
using SchoolDatabaseManager.Commands;
using SchoolDatabaseManager.Database;
using System.Runtime.InteropServices;

namespace SchoolDatabaseManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseHelper.InitilizeDatabase();

            Dictionary<string, Action<string[]>> commands = CommandManager.GetCommands();

            bool shouldExit = false;
            while (!shouldExit)
            {
                // Get input from user
                Console.Write(">> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;

                // Parse input
                string[] split = input.Split(" ");
                string commandName = split[0];
                string[] arguments = split.Skip(1).ToArray();

                // Exit command
                if (commandName == "exit")
                {
                    shouldExit = true;
                    continue;
                }

                // Try to get the command
                if (!commands.TryGetValue(commandName, out Action<string[]>? command))
                {
                    // Command doesn't exist
                    Console.WriteLine("Invalid command.");
                    continue;
                }

                try
                {
                    // Try to run the command
                    command(arguments);
                } 
                catch (Exception exception)
                {
                    // Something went wrong
                    Console.WriteLine($"Error when executing command {commandName}");
                    Console.WriteLine(exception);
                }
            }
        }
    }
}
