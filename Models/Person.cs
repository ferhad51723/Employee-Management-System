public abstract class Person
{
    public int Id { get; private set; }
    private string _Name = "N/A";

    public string Name
    {
        get { return _Name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NameEmptyException("Ad boş ola bilməz!");
            }
            if (value.Length < 3 || value.Length > 25)
            {
                throw new NameLengthException("Ad 3-25 simvol arası olmalıdır!");
            }
            _Name = value;
        }
    }

    public Person(int id, string name)
    {
        this.Id = id;
        this.Name = name; 
    }

    public abstract void PrintInfo();
}