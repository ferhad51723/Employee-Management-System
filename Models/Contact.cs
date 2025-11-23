public struct Contact
{
    public string OfficeNumber;
    public int Floor;

    public Contact(string officeNumber, int floor)
    {
        if (string.IsNullOrWhiteSpace(officeNumber))
        {
            throw new InvalidWorkInfoException("Office Number boş ola bilməz!");
        }
        if (floor <= 0)
        {
            throw new InvalidWorkInfoException("Floor 0-dan böyük olmalıdır!");
        }
        
        this.OfficeNumber = officeNumber;
        this.Floor = floor;
    }
    
    public override string ToString()
    {
        return $"Office: {OfficeNumber ?? "N/A"}, Floor: {Floor}";
    }
}