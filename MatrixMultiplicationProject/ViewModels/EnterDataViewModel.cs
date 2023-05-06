using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MatrixMultiplicationProject.Models;

namespace MatrixMultiplicationProject.ViewModels;

public partial class EnterDataViewModel : ObservableObject
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

    [RelayCommand]
    public void ConfirmMatrixSizes()
    {
        FirstMatrix = new long[FirstMatrixRows, FirstMatrixColumns];
        SecondMatrix = new long[SecondMatrixRows, SecondMatrixColumns];
    }
}