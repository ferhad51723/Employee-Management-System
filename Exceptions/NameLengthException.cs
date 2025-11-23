using System;
using System.Runtime.Serialization;

[Serializable]
public class NameLengthException : Exception
{
    // Constructors
    public NameLengthException(string message) 
        : base(message)
    { }
}