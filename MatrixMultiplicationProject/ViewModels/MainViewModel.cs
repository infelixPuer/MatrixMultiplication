using System;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
    [NotifyCanExecuteChangedFor(nameof(SendMessagesCommand))]
    private long[,]? _firstMatrix;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SendMessagesCommand))]
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

        Debug.WriteLine($"First matrix 0-0: {FirstMatrix[0, 0]}; 1-0: {FirstMatrix[1, 0]}, 2-0: {FirstMatrix[2, 0]}");
        Debug.WriteLine($"Second matrix 0-0: {SecondMatrix[0, 0]}; 1-0: {SecondMatrix[1, 0]}, 2-0: {SecondMatrix[2, 0]}");

        WeakReferenceMessenger.Default.Send(new Matrices(FirstMatrix!, SecondMatrix!));
    }

    partial void OnResultChanged(long[,]? value)
    {
        MessageBox.Show($"{Result}");
    }

    [RelayCommand]
    private void ConfirmMatrixSizes()
    {
        FirstMatrix = new long[FirstMatrixRows, FirstMatrixColumns];
        SecondMatrix = new long[SecondMatrixRows, SecondMatrixColumns];
        IsConfirmed = true;
        Result = new long[FirstMatrixRows, SecondMatrixColumns];
    }

    private bool CanClick() 
        => FirstMatrix is not null && SecondMatrix is not null;
    

    //[RelayCommand(CanExecute = nameof(CanClick))]
    //private async void MultiplyMatricesAsync()
    //{
    //    var token = TokenSource.Token;
    //    Result = await MatrixMultiplicationBase.MultiplyAsync(FirstMatrix!, SecondMatrix!, token);
    //}

    [RelayCommand(CanExecute = nameof(CanClick))]
    private void SendMessages()
    {
        WeakReferenceMessenger.Default.Send(TokenSource);
    }

    [RelayCommand]
    private void GenerateImage()
    {
        var bitmap = new Bitmap(Result!.GetLength(0), Result.GetLength(1));
        var max = GetMax(Result);
        var multiplier = 255.0M / max;

        for (int i = 0; i < Result.GetLength(0); i++)
        {
            for (int j = 0; j < Result.GetLength(1); j++)
            {
                bitmap.SetPixel(i, j, Color.FromArgb(255, Color.FromArgb(1, (int)(Result[i, j] * multiplier), 127, 0)));
                
            }
        }

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