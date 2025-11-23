public class Employee : Person, IPrintable
{
    public Position? Position { get; set; }

    private decimal? _Salary;
    public decimal? Salary
    {
        get { return _Salary; }
        set
        {
            if (value.HasValue && value.Value <= 0)
            {
                throw new InvalidSalaryException("Maaş 0-dan böyük olmalıdır!");
            }
            _Salary = value;
        }
    }
    private string _Department = "";
    public string Department
    {
        get { return _Department; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NameEmptyException("Department adı boş ola bilməz!");
            }
            if (value.Length < 5 || value.Length > 6)
            {
                throw new NameLengthException("Department adı minimum 5, maksimum 6 simvol olmalıdır!");
            }
            _Department = value;
        }
    }
    public DateTime HireDate { get; private set; }
    public Contact WorkInfo { get; set; }

    public Employee(int id, string name, decimal? salary, string department, Contact workInfo)
        : base(id, name)
    {
        this.Salary = salary;
        this.Department = department;
        this.WorkInfo = workInfo;
        this.HireDate = DateTime.Now; 
    }

    public override void PrintInfo()
    {
        string? position = Position.HasValue ? Position.ToString() : "N/A";
        string salary = Salary.HasValue ? Salary.Value.ToString("C") : "N/A"; 
        string contact = WorkInfo.ToString(); 

        string hireDate = HireDate.ToString("d MMMM yyyy");
        string workInfo = WorkInfo.ToString() ?? "N/A";

        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Position: {position}");
        Console.WriteLine($"Department: {Department}");
        Console.WriteLine($"Salary: {salary}");
        Console.WriteLine($"Hire Date: {hireDate}");
        Console.WriteLine($"Work Info: {workInfo}");
    }
}