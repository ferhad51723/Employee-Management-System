using System.Collections.Generic;
using System.Linq;

public class EmployeeService
{
    private List<Employee> _employees = new List<Employee>();
    private int _nextId = 1;

    public void AddEmployee(string name, decimal? salary, string department, string officeNumber, int floor)
    {
        if (_employees.Any(e => e.Id == _nextId))
        {
            throw new DuplicateEmployeeException($"ID {_nextId} ilə işçi artıq mövcuddur!");
        }

        Contact contactInfo = new Contact(officeNumber, floor); 

        Employee newEmployee = new Employee(_nextId, name, salary, department, contactInfo);
        
        _employees.Add(newEmployee);
        _nextId++;
        Console.WriteLine($"SUCCESS: İşçi {newEmployee.Name} (ID: {newEmployee.Id}) əlavə edildi.");
    }

    public void RemoveEmployee(int id)
    {
        Employee employeeToRemove = _employees.FirstOrDefault(e => e.Id == id);
        
        if (employeeToRemove == null)
        {
            throw new EmployeeNotFoundException($"ID {id} ilə işçi tapılmadı.");
        }

        _employees.Remove(employeeToRemove);
        Console.WriteLine($"SUCCESS: İşçi {employeeToRemove.Name} silindi.");
    }
    
    public void ListEmployees()
    {
        if (!_employees.Any())
        {
            Console.WriteLine("Kolleksiyada heç bir işçi yoxdur.");
            return;
        }

        foreach (var employee in _employees)
        {
            employee.PrintInfo(); 
        }
    }
    
    public void SortEmployeesByName()
    {
        var sortedList = _employees.OrderBy(e => e.Name).ToList();
        
        Console.WriteLine("\n--- Adına Görə Sıralanmış İşçilər ---");
        foreach (var emp in sortedList)
        {
            emp.PrintInfo();
        }
    }
    
}
