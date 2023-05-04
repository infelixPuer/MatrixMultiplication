namespace MatrixMultiplicationProject.Models;

public class Matrix
{
    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public long[,] Values { get; private set; }

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Values = new long[rows, columns];
    }

    public Matrix(long[,] values)
    {
        Values = values;
        Rows = values.GetLength(0);
        Columns = values.GetLength(1);
    }

    public Matrix()
    {
        Rows = 0;
        Columns = 0;
        Values = new long[Rows, Columns];
    }

    public void FillWithNumber(int num)
    {
        for (int i = 0; i < Values.GetLength(0); i++)
            for (int j = 0; j < Values.GetLength(1); j++)
                Values[i, j] = num;
    }

    public void FillWithSumOfCoords()
    {
        for (int i = 0; i < Values.GetLength(0); i++)
            for (int j = 0; j < Values.GetLength(1); j++)
                Values[i, j] = i + j;
    }

    public void FillWithProductOfCoords()
    {
        for (int i = 0; i < Values.GetLength(0); i++)
            for (int j = 0; j < Values.GetLength(1); j++)
                Values[i, j] = i * j;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        var result = new long[a.Columns, b.Rows];

        for (int rows = 0; rows < result.GetLength(0); rows++)
        {
            for (int cols = 0; cols < result.GetLength(1); cols++)
            {
                result[rows, cols] = DotProduct(GetRow(a.Values, rows), GetColumn(b.Values, cols));
            }
        }

        return new Matrix(result);
    }

    public static long DotProduct(long[] a, long[] b)
    {
        long result = 0;

        for (int i = 0; i < a.Length; i++)
            result += a[i] * b[i];

        return result;
    }

    public static long[] GetColumn(long[,] array, int col)
    {
        var result = new long[array.GetLength(0)];

        for (int i = 0; i < result.Length; i++)
            result[i] = array[i, col];

        return result;
    }

    public static long[] GetRow(long[,] array, int row)
    {
        var result = new long[array.GetLength(0)];

        for (int i = 0; i < result.Length; i++)
            result[i] = array[row, i];

        return result;
    }
}