using System.Linq;
using System.Threading.Tasks;
using MatrixMultiplicationProject.Exceptions;

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
        if (a.Columns != b.Rows)
            throw new IncompatibleMatricesException($"First matrix has {a.Columns} columns, but second matrix has {b.Rows} rows!");

        var result = new long[a.Rows, b.Columns];
        var divisor = GetDivisor(result).ToString();

        for (int i = 2; i < a.Rows.ToString().Length - 1; i++)
            divisor += "0";

        var taskArray = new Task[int.Parse(divisor)];

        var count = a.Rows / int.Parse(divisor);

        for (int i = 0; i < taskArray.Length; i++)
        {
            var iterator = i;

            taskArray[i] = Task.Factory.StartNew(() =>
            {
                for (int r = iterator * count; r < count * (iterator + 1); r++)
                    for (int c = 0; c < result.GetLength(1); c++)
                        result[r, c] = DotProduct(GetRow(a.Values, r), GetColumn(b.Values, c));
            });
        }

        Task.WaitAll(taskArray);

        return new Matrix(result);
    }

    private static int GetDivisor(long[,] array)
    {
        var arrayLength = array.GetLength(0);

        return arrayLength < 10 ? 1 : 5;
    }

    public static long DotProduct(long[] a, long[] b) => 
        a.Select((t, i) => t * b[i]).Sum();


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