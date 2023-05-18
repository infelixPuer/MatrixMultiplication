using System.Windows;
using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationProject.Views;

public partial class MainWindow
{
    private readonly object _fillWithNumberView;
    private readonly object _fillWithCoordSumView;
    private readonly object _fillWithCoordProductView;
    private readonly object _fillWithRandomNumberView;
    private readonly object _calculateView;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
        _fillWithNumberView = new FillWithNumberView();
        _fillWithCoordSumView = new FillWithCoordSumView();
        _fillWithCoordProductView = new FillWithCoordsProductView();
        _fillWithRandomNumberView = new FillWithRandomNumbersView();
        _calculateView = new CalculateView();
    }
    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = null;
    }

    private void FillWithNumberButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithNumberView;
    }

    private void FillWithRandomNumbersButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithRandomNumberView;
    }


    private void FillWithCoordsSumButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithCoordSumView;
    }

    private void FillWithCoordsProductButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithCoordProductView;
    }

    private void CalculateButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _calculateView;
    }
}