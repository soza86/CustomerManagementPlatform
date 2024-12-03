namespace TestConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var employee = new Employee { Name = "John Doe" };
            var manager = new Manager { Name = "Jane Doe" };

            var person = new Person();
            person.PrintInfo(employee);
            person.PrintInfo(manager);
        }
    }
}
