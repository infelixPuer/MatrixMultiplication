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

    public static async Task<long[,]> MultiplyAsync(Matrices matrices, IProgress<double> progress, CancellationToken token)
    {
        var result = new long[matrices.FirstMatrix.GetLength(0), matrices.SecondMatrix.GetLength(1)];
        var size = result.GetLength(0) * result.GetLength(1);
        var tasksCount = GetTaskCount(size);
        var tasks = new Task[tasksCount];
        var rowPerTask = (int)(result.GetLength(0) / tasksCount);
        var progressStep = 1.0/ size;
        var currentProgress = 0.0;
        var totalProgress = 0.0;

        try
        {
            for (int i = 0; i < tasksCount; i++)
            {
                var iterator = i;

                tasks[iterator] = Task.Run(() =>
                {
                    for (int r = iterator * rowPerTask; r < rowPerTask * (iterator + 1); r++)
                    {
                        for (int c = 0; c < result.GetLength(1); c++)
                        {
                            result[r, c] = DotProduct(GetRow(matrices.FirstMatrix, r), GetColumn(matrices.SecondMatrix, c));

                            lock (s_addingToProgressLocker)
                                currentProgress += progressStep;
                        }

                        lock (s_reportingProgressLocker)
                        {
                            totalProgress += currentProgress;
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
            progress.Report(totalProgress);
            return null;
        }

        if (totalProgress < 1.0)
            progress.Report(1.0 - totalProgress);

        return result;
    }

    private static int GetTaskCount(int size) 
        => size < 1_000_000 ? (size < 5_000 ? 1 : size / 5_000) : 200;

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