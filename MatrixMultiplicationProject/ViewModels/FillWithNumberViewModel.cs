using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace MatrixMultiplicationProject.ViewModels;

public partial class FillWithNumberViewModel : ObservableObject
{
    [ObservableProperty]
    private long _number1;

    [ObservableProperty]
    private long _number2;

    [ObservableProperty] 
    private Action<long[,], long[,]>? _option;

    [RelayCommand]
    private void ProcessFillingOption()
    {
        Option = (long[,] matrix1, long[,] matrix2) =>
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
                for (int j = 0; j < matrix1.GetLength(1); j++)
                    matrix1[i, j] = Number1;

            for (int i = 0; i < matrix2.GetLength(0); i++)
                for (int j = 0; j < matrix2.GetLength(1); j++)
                    matrix2[i, j] = Number2;
        };

        WeakReferenceMessenger.Default.Send(Option);

        MessageBox.Show($"Filling option is send!" +
                        $"\nFirst matrix will be filled with {Number1}" +
                        $"\nSecond matrix will be filled with {Number2}");
    }
}