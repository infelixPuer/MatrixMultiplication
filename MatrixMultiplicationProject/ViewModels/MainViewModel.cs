using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MatrixMultiplicationProject.Models;

namespace MatrixMultiplicationProject.ViewModels;

public partial class MainViewModel : ObservableObject
{
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

    public MainViewModel()
    {
        TokenSource = new CancellationTokenSource();
    }

    [RelayCommand]
    private void ConfirmMatrixSizes()
    {
        FirstMatrix = new long[FirstMatrixRows, FirstMatrixColumns];
        SecondMatrix = new long[SecondMatrixRows, SecondMatrixColumns];
    }


    [RelayCommand]
    private void MultiplyMatricesAsync()
    {
        var token = TokenSource.Token;
        Result = MatrixMultiplicationBase.MultiplyAsync(FirstMatrix, SecondMatrix, token).Result;
    }
}