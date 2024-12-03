namespace TestConsoleApp
{
    public class Person
    {
        public void PrintInfo(Employee employee)
        {
            Console.WriteLine($"Employee Name: {employee.Name}");
        }

        public void PrintInfo(Manager manager)
        {
            Console.WriteLine($"Manager Name: {manager.Name}");
        }
    }
}
