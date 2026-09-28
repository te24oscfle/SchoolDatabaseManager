
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
                Console.Write(">> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;

                string[] split = input.Split(" ");
                string commandName = split[0];
                string[] arguments = split.Skip(1).ToArray();

                if (commandName == "exit")
                {
                    shouldExit = true;
                    continue;
                }

                if (!commands.TryGetValue(commandName, out Action<string[]>? command))
                {
                    Console.WriteLine("Invalid command.");
                    continue;
                }

                try
                {
                    command(arguments);
                } 
                catch (Exception exception)
                {
                    Console.WriteLine($"Error when executing command {commandName}");
                    Console.WriteLine(exception);
                }
            }
        }
    }
}
