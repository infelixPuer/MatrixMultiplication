using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MatrixMultiplicationProject.Exceptions;
using MatrixMultiplicationProject.Models;

namespace MatrixMultiplicationProject.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    [ObservableProperty]
    private Action<long[,], long[,]>? _fillingOption;

    [ObservableProperty]
    private int _firstMatrixRows;

    [ObservableProperty]
    private int _firstMatrixColumns;
    
    [ObservableProperty]
    private int _secondMatrixRows;
    
    [ObservableProperty]
    private int _secondMatrixColumns;

    [ObservableProperty]
    private long[,]? _firstMatrix;

    [ObservableProperty]
    private long[,]? _secondMatrix;

    [ObservableProperty] 
    private long[,]? _result;

    [ObservableProperty] 
    private CancellationTokenSource _tokenSource;

    [ObservableProperty] 
    private bool _isConfirmed;

    public MainViewModel()
    {
        TokenSource = new CancellationTokenSource();
        WeakReferenceMessenger.Default.Register<Action<long[,], long[,]>>(this, (r, m) =>
        {
            FillingOption = m;
        });

        WeakReferenceMessenger.Default.Register<long[,]>(this, (r, m) =>
        {
            Result = m;
        });
    }

    partial void OnFillingOptionChanged(Action<long[,], long[,]>? value)
    {
        FillingOption?.Invoke(FirstMatrix!, SecondMatrix!);

        WeakReferenceMessenger.Default.Send(new Matrices(FirstMatrix!, SecondMatrix!));
    }

    [RelayCommand]
    private void ConfirmMatrixSizes()
    {
        try
        {
            if (FirstMatrixColumns != SecondMatrixRows)
                throw new IncompatibleMatricesException($"First matrix columns number must be equal to second matrix row number." +
                                                        $"\nYour input was:" +
                                                        $"\nFirst matrix columns: {FirstMatrixColumns}" +
                                                        $"\nSecond matrix rows: {SecondMatrixRows}");

            FirstMatrix = new long[FirstMatrixRows, FirstMatrixColumns];
            SecondMatrix = new long[SecondMatrixRows, SecondMatrixColumns];
            IsConfirmed = true;
            Result = new long[FirstMatrixRows, SecondMatrixColumns];
        }
        catch (IncompatibleMatricesException e)
        {
            MessageBox.Show(e.Message);
        }
    }

    [RelayCommand]
    private void GenerateImage()
    {
        var bitmap = new Bitmap(Result!.GetLength(0), Result.GetLength(1));
        var max = GetMax(Result);
        var multiplier = 255.0M / max;

        for (int i = 0; i < Result.GetLength(0); i++)
            for (int j = 0; j < Result.GetLength(1); j++)
                bitmap.SetPixel(i, j, Color.FromArgb(255, (int)(Result[i, j] * multiplier), 0, 0)); 
                

        bitmap.Save("image.bmp");
    }

    private long GetMax(long[,] matrix)
    {
        var max = long.MinValue;

        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                max = matrix[i, j] > max ? matrix[i, j] : max;

        return max;
    }
}