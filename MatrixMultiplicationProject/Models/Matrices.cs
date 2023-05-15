namespace MatrixMultiplicationProject.Models;

public class Matrices
{
    public long[,] FirstMatrix { get; set; }
    public long[,] SecondMatrix { get; set; }

    public Matrices(long[,] firstMatrix, long[,] secondMatrix)
    {
        FirstMatrix = firstMatrix;
        SecondMatrix = secondMatrix;
    }
}