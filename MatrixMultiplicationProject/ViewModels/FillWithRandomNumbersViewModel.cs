using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace MatrixMultiplicationProject.ViewModels;

public partial class FillWithRandomNumbersViewModel : ObservableObject
{
    [ObservableProperty]
    private int _min;

    [ObservableProperty]
    private int _max;

    [ObservableProperty]
    private Action<long[,], long[,]>? _option;

    [RelayCommand]
    private void ProcessFillingOption()
    {
        var r = new Random();

        Option = (long[,] matrix1, long[,] matrix2) =>
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
                for (int j = 0; j < matrix1.GetLength(1); j++)
                    matrix1[i, j] = r.Next(Min, Max);

            for (int i = 0; i < matrix2.GetLength(0); i++)
                for (int j = 0; j < matrix2.GetLength(1); j++)
                    matrix2[i, j] = r.Next(Min, Max);
        };

        WeakReferenceMessenger.Default.Send(Option);
    }
}