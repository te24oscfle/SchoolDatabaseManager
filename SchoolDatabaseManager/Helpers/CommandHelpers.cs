using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDatabaseManager.Helpers
{
    public static class CommandHelpers
    {
        public static void Add<T>(List<T> items, Func<string[], T> factory, string[] arguments)
        {
            T item = factory(arguments);
            items.Add(item);
        }

        public static void Get<T>(List<T> items)
        {
            foreach(T item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
