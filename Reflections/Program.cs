using System;
namespace Reflections
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 15;
            Type type = a.GetType();
            Console.WriteLine(type.FullName);
        }
    }
}
