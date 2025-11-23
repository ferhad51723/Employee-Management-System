public class FileLoadException : Exception
{
    public FileLoadException(string message, Exception innerException) : base(message, innerException) { }
}