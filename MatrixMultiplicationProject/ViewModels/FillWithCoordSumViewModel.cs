using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace MatrixMultiplicationProject.ViewModels;

public partial class FillWithCoordSumViewModel : ObservableObject
{
    [ObservableProperty] 
    private Action<long[,], long[,]>? _option;

    [RelayCommand]
    private void SendFillingOption()
    {
        Option = (long[,] matrix1, long[,] matrix2) =>
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
                for (int j = 0; j < matrix1.GetLength(1); j++)
                    matrix1[i, j] = i + j;

            for (int i = 0; i < matrix2.GetLength(0); i++)
                for (int j = 0; j < matrix2.GetLength(1); j++)
                    matrix2[i, j] = i + j;
        };

        WeakReferenceMessenger.Default.Send(Option);
    }
}