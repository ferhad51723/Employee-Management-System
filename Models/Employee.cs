public class Employee : Person, IPrintable
{
    public Position? Position { get; set; }
    public decimal? Salary { get; set; }
    public string Department { get; set; }
    public Contact WorkInfo { get; set; }
    public DateTime HireDate { get; private set; }

    public Employee(int id, string name, decimal? salary, string department, Contact workInfo)
        : base(id, name)
    {
        if (salary.HasValue && salary.Value <= 0)
        {
            throw new InvalidSalaryException("Maaş müsbət olmalıdır!");
        }
        
        this.Salary = salary;
        this.Department = department;
        this.WorkInfo = workInfo;
        this.HireDate = DateTime.Now;
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"ID: {this.Id}");
        Console.WriteLine($"Name: {this.Name}");
        
        Console.WriteLine($"Position: {Position.HasValue ? Position.ToString() : "N/A"}");
        Console.WriteLine($"Salary: {Salary.HasValue ? Salary.Value.ToString("C") : "N/A"}");
        Console.WriteLine($"Department: {Department ?? "N/A"}");
        Console.WriteLine($"Work Info: {WorkInfo}");
        Console.WriteLine($"Hire Date: {HireDate.ToShortDateString()}");
    }
}