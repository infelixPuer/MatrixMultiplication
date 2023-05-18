using System;
using System.Threading;
using System.Threading.Tasks;
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
    private double? _doneWork = 0;

    [ObservableProperty] 
    private double _progressStep;

    private readonly CancellationTokenSource _tokenSource = new();

    partial void OnResultChanged(long[,]? value)
    {
        WeakReferenceMessenger.Default.Send(Result!);
    }

    public CalculateViewModel()
    {
        WeakReferenceMessenger.Default.Register<Matrices>(this, (r, m) =>
        {
            Matrices = m;
            Result = new long[m.FirstMatrix.GetLength(0), m.SecondMatrix.GetLength(1)];
            TotalWork = 1;
        });
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task MultiplyMatricesAsync(CancellationToken token)
    {
        DoneWork = 0;

        var progress = new Progress<double>(task =>
        {
            DoneWork += task;
        });

        ProgressStep = 1.0 / (Result!.GetLength(0) * Result.GetLength(1));
        Token = _tokenSource.Token;
        Result = await MatrixMultiplicationBase.MultiplyAsync(Matrices!, progress, token);
    }
}