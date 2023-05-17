using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MatrixMultiplicationProject.Models;

namespace MatrixMultiplicationProject.ViewModels;

public partial class CalculateViewModel : ObservableObject
{
    [ObservableProperty]
    private Matrices? _matrices;

    [ObservableProperty]
    private CancellationToken _token;

    [ObservableProperty] 
    private long[,]? _result;

    [ObservableProperty]
    private long? _totalWork = 0;

    [ObservableProperty] 
    private decimal? _doneWork = 0;

    [ObservableProperty] 
    private decimal _progressStep;

    private readonly CancellationTokenSource _tokenSource = new();

    partial void OnResultChanged(long[,]? value)
    {
        MessageBox.Show($"{Result} changed!");
        WeakReferenceMessenger.Default.Send(Result!);
    }

    partial void OnDoneWorkChanged(decimal? value)
    {
        Debug.WriteLine(DoneWork);
    }

    public CalculateViewModel()
    {
        WeakReferenceMessenger.Default.Register<Matrices>(this, (r, m) =>
        {
            Matrices = m;
            Result = new long[m.FirstMatrix.GetLength(0), m.SecondMatrix.GetLength(1)];
            //TotalWork = MatrixMultiplicationBase.GetTasksNumber(Result!, 1, 200, 5_000);
            TotalWork = 1;
        });
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task MultiplyMatricesAsync(CancellationToken token)
    {
        var progress = new Progress<decimal>(task =>
        {
            DoneWork += task;
        });

        ProgressStep = 1.0M / (Result.GetLength(0) * Result.GetLength(1));
        Token = _tokenSource.Token;
        Result = await MatrixMultiplicationBase.MultiplyAsync(Matrices!, progress, token);
    }
}