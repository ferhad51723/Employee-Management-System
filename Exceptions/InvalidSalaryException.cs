using System;
using System.Runtime.Serialization;

[Serializable]

public class InvalidSalaryException : Exception
{
    public InvalidSalaryException(string message) : base(message) { }
}