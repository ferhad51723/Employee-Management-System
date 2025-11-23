using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class EmployeeService
{
    private List<Employee> _employees = new List<Employee>();
    private const string DataFilePath = "Data/Employees.json";
    private int _nextId = 1; 

    public EmployeeService()
    {
        LoadFromFile();
        _nextId = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
    }

    public void LoadFromFile()
    {
        if (!File.Exists(DataFilePath))
        {
            _employees = new List<Employee>();
            return;
        }

        try
        {
            string jsonString = File.ReadAllText(DataFilePath);
            _employees = JsonConvert.DeserializeObject<List<Employee>>(jsonString) ?? new List<Employee>();
            Console.WriteLine($"INFO: {DataFilePath} faylından {_employees.Count} işçi yükləndi.");
        }
        catch (Exception ex)
        {
            throw new FileLoadException($"Məlumatlar {DataFilePath} faylından yüklənərkən səhv baş verdi.", ex);
        }
    }

    public void SaveToFile()
    {
        string directory = Path.GetDirectoryName(DataFilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        string jsonString = JsonConvert.SerializeObject(_employees, Formatting.Indented);
        File.WriteAllText(DataFilePath, jsonString);
        Console.WriteLine("INFO: Məlumatlar JSON faylına uğurla yazıldı.");
    }

    public void AddEmployee(string name, decimal? salary, string department, string officeNumber, int floor)
    {
        if (_employees.Any(e => e.Id == _nextId))
        {
            throw new DuplicateEmployeeException($"ID {_nextId} ilə işçi artıq mövcuddur.");
        }

        Contact contactInfo = new Contact(officeNumber, floor);

        Employee newEmployee = new Employee(_nextId, name, salary, department, contactInfo);
        
        _employees.Add(newEmployee);
        _nextId++;
        
        SaveToFile(); // Faylı yenilə
        Console.WriteLine($"SUCCESS: İşçi {newEmployee.Name} ({newEmployee.Id}) uğurla əlavə edildi.");
    }
    
    public void RemoveEmployee(int id)
    {
        Employee employeeToRemove = _employees.FirstOrDefault(e => e.Id == id);

        if (employeeToRemove == null)
        {
            throw new EmployeeNotFoundException($"ID {id} ilə işçi tapılmadı.");
        }

        _employees.Remove(employeeToRemove);
        SaveToFile();
        Console.WriteLine($"SUCCESS: İşçi {employeeToRemove.Name} silindi.");
    }

    public Employee SearchEmployee(int id)
    {
        Employee employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            throw new EmployeeNotFoundException($"ID {id} ilə işçi tapılmadı.");
        }
        return employee;
    }
    
    public void UpdateEmployee(int id, Position? newPosition)
    {
        Employee employeeToUpdate = _employees.FirstOrDefault(e => e.Id == id);
        
        if (employeeToUpdate == null)
        {
            throw new EmployeeNotFoundException($"ID {id} ilə işçi tapılmadı.");
        }

        employeeToUpdate.Position = newPosition;
        SaveToFile();
        Console.WriteLine($"SUCCESS: İşçi {employeeToUpdate.Name}-in vəzifəsi {newPosition} olaraq yeniləndi.");
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
    
    public List<Employee> SortEmployees(string criteria)
    {
        IEnumerable<Employee> sortedList = _employees;

        switch (criteria.ToLower())
        {
            case "id":
                sortedList = _employees.OrderBy(e => e.Id);
                break;
            case "name":
                sortedList = _employees.OrderBy(e => e.Name);
                break;
            case "hiredate":
                sortedList = _employees.OrderBy(e => e.HireDate);
                break;
            case "salary":
                sortedList = _employees.OrderBy(e => e.Salary);
                break;
            default:
                Console.WriteLine("ERROR: Yanlış sıralama kriteriyası. (id, name, hiredate, salary)");
                return _employees;
        }
        return sortedList.ToList();
    }
    
    public List<Employee> FilterEmployees(string nameFilter, decimal? minSalary, decimal? maxSalary)
    {
        IEnumerable<Employee> filteredList = _employees
            .Where(e => string.IsNullOrEmpty(nameFilter) || e.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
        
        if (minSalary.HasValue)
        {
            filteredList = filteredList.Where(e => e.Salary.HasValue && e.Salary.Value >= minSalary.Value);
        }
        if (maxSalary.HasValue)
        {
            filteredList = filteredList.Where(e => e.Salary.HasValue && e.Salary.Value <= maxSalary.Value);
        }

        return filteredList.ToList();
    }
}