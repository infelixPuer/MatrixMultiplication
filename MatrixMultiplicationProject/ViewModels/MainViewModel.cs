using System;
using System.Threading;
using System.Windows;
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
    [NotifyCanExecuteChangedFor(nameof(MultiplyMatricesAsyncCommand))]
    private long[,]? _firstMatrix;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(MultiplyMatricesAsyncCommand))]
    private long[,]? _secondMatrix;

    [ObservableProperty] 
    private long[,]? _result;

    [ObservableProperty] 
    private long _resultItems;

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
    }

    partial void OnFillingOptionChanged(Action<long[,], long[,]>? value)
    {
        FillingOption?.Invoke(FirstMatrix!, SecondMatrix!);
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
        ResultItems = FirstMatrixRows * SecondMatrixColumns;
    }

    private bool CanClick() 
        => FirstMatrix is not null && SecondMatrix is not null;
    

    [RelayCommand(CanExecute = nameof(CanClick))]
    private async void MultiplyMatricesAsync()
    {
        var token = TokenSource.Token;
        Result = await MatrixMultiplicationBase.MultiplyAsync(FirstMatrix!, SecondMatrix!, token);
    }
}