using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MatrixMultiplicationProject.Models;

public static class MatrixMultiplicationBase
{
    private static readonly object s_addingToProgressLocker = new();
    private static readonly object s_reportingProgressLocker = new();

    public static void FillWithNumber(this long[,] matrix, long number)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                matrix[i, j] = number;
    }

    public static void FillWithCoordsSum(this long[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0;j < matrix.GetLength(1); j++)
                matrix[i, j] = i + j;
    }

    public static void FillWithCoordsProduct(this long[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                matrix[i, j] = i * j;
    }

    public static async Task<long[,]> MultiplyAsync(Matrices matrices, IProgress<decimal> progress, CancellationToken token)
    {
        var matrix1 = matrices.FirstMatrix;
        var matrix2 = matrices.SecondMatrix;
        var result = new long[matrix1.GetLength(0), matrix2.GetLength(1)];
        var tasksCount = GetDivisor(result).ToString();

        for (int i = 2; i < matrix1.GetLength(1).ToString().Length - 1; i++)
            tasksCount += "0";

        var tasks = new Task[int.Parse(tasksCount)];

        var rowPerTask = matrix1.GetLength(0) / int.Parse(tasksCount);
        var progressStep = 1.0M / (result.GetLength(0) * result.GetLength(1));
        var currentProgress = 0.0M;

        try
        {
            for (int i = 0; i < int.Parse(tasksCount); i++)
            {
                var iterator = i;

                tasks[iterator] = Task.Run(() =>
                {
                    for (int r = iterator * rowPerTask; r < rowPerTask * (iterator + 1); r++)
                    {
                        for (int c = 0; c < result.GetLength(1); c++)
                        {
                            result[r, c] = DotProduct(GetRow(matrix1, r), GetColumn(matrix2, c));

                            lock (s_addingToProgressLocker)
                                currentProgress += progressStep;
                        }

                        lock (s_reportingProgressLocker)
                        {
                            progress.Report(currentProgress);
                            currentProgress = 0;
                        }
                    }
                }, token);
            }

            await Task.WhenAll(tasks);
        }
        catch (OperationCanceledException e)
        {
            MessageBox.Show("Operation was cancelled!");
            progress.Report(-1);
        }

        return result;
    }

    private static int GetDivisor(long[,] array)
    {
        var arrayLength = array.GetLength(0);

        return arrayLength < 10 ? 1 : 5;
    }

    private static long DotProduct(long[] a, long[] b) =>
        a.Select((t, i) => t * b[i]).Sum();


    private static long[] GetColumn(long[,] array, int col)
    {
        var result = new long[array.GetLength(0)];

        for (int i = 0; i < result.Length; i++)
            result[i] = array[i, col];

        return result;
    }

    private static long[] GetRow(long[,] array, int row)
    {
        var result = new long[array.GetLength(1)];

        for (int i = 0; i < result.Length; i++)
            result[i] = array[row, i];

        return result;
    }
}