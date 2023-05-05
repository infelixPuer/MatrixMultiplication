using System;

namespace MatrixMultiplicationProject.Exceptions;

public class IncompatibleMatricesException : Exception
{
    public IncompatibleMatricesException()
    {
    }

    public IncompatibleMatricesException(string message) :base(message)
    {
    }
}