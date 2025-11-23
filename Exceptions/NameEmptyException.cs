using System;
using System.Runtime.Serialization;

[Serializable]
public class NameEmptyException : Exception
{
    // Constructors
    public NameEmptyException(string message) 
        : base(message)
    { }
}