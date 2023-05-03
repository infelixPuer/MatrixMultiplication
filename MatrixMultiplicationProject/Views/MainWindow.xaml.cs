using System.Windows;

namespace MatrixMultiplicationProject.Views;

public partial class MainWindow
{
    private readonly object _fillWithNumberView;
    private readonly object _fillWithCoordSumView;
    private readonly object _fillWithCoordProductView;

    public MainWindow()
    {
        InitializeComponent();
        _fillWithNumberView = new FillWithNumberView();
        _fillWithCoordSumView = new FillWithCoordSumView();
        _fillWithCoordProductView = new FillWithCoordsProductView();
    }
    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = null;
    }

    private void FillWithNumberButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithNumberView;
    }


    private void FillWithCoordsSumButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithCoordSumView;
    }

    private void FillWithCoordsProductButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithCoordProductView;
    }
}