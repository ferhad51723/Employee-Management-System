using System;
using System.Runtime.Serialization;

[Serializable]

public class InvalidWorkInfoException : Exception
{
    public InvalidWorkInfoException(string message) : base(message) { }
}