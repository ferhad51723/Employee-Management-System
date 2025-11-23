using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        EmployeeService service = new EmployeeService();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n*** İşçi İdarəetmə Sistemi (OOP Beginner) ***");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Remove Employee");
            // ... Digər menyu variantları ...
            Console.WriteLine("5. List Employees");
            Console.WriteLine("6. Sort Employees (by Name)");
            Console.WriteLine("0. Exit");
            Console.Write("Seçiminizi daxil edin: ");

            string choice = Console.ReadLine();
            
            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("ID avtomatik verilir. Ad: ");
                        string name = Console.ReadLine();
                        Console.Write("Maaş (decimal, boş buraxınsa N/A): ");
                        // Sadəlik üçün TryParse
                        decimal? salary = decimal.TryParse(Console.ReadLine(), out decimal s) ? s : (decimal?)null;
                        Console.Write("Department: ");
                        string dept = Console.ReadLine();
                        Console.Write("Office No: ");
                        string office = Console.ReadLine();
                        Console.Write("Mərtəbə (Floor > 0): ");
                        int floor = int.Parse(Console.ReadLine());
                        
                        service.AddEmployee(name, salary, dept, office, floor);
                        break;
                    
                    case "2":
                        Console.Write("Silinəcək işçinin ID-sini daxil edin: ");
                        if (int.TryParse(Console.ReadLine(), out int removeId))
                        {
                            service.RemoveEmployee(removeId);
                        }
                        break;
                        
                    case "5":
                        service.ListEmployees();
                        break;
                        
                    case "6":
                        service.SortEmployeesByName();
                        break;

                    case "0":
                        running = false;
                        break;
                        
                    default:
                        Console.WriteLine("Yanlış seçim.");
                        break;
                }
            }
            catch (Exception ex) when (ex is NameEmptyException || ex is InvalidSalaryException || ex is InvalidWorkInfoException || ex is EmployeeNotFoundException || ex is DuplicateEmployeeException)
            {
                Console.WriteLine($"\n--- XƏTA: {ex.GetType().Name} ---");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gözlənilməyən xəta: {ex.Message}");
            }
        }
    }
}