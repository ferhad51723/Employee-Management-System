public struct Contact
{
    public string OfficeNumber { get; set; }
    public int Floor { get; set; }

    public Contact(string officeNumber, int floor)
    {
        if (string.IsNullOrWhiteSpace(officeNumber))
        {
            throw new InvalidWorkInfoException("OfficeNumber boş ola bilməz!");
        }

        if (floor <= 0)
        {
            throw new InvalidWorkInfoException("Floor dəyəri 0-dan böyük olmalıdır!");
        }

        this.OfficeNumber = officeNumber;
        this.Floor = floor;
    }

    public override string ToString()
    {
        return $"Office: {OfficeNumber}, Floor: {Floor}";
    }
}