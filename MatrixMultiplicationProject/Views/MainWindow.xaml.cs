using System.Windows;

namespace MatrixMultiplicationProject.Views;

public partial class MainWindow
{
    private object _fillWithNumberView;
    private object _fillWithCoordSumView;

    public MainWindow()
    {
        InitializeComponent();
        _fillWithNumberView = new FillWithNumberView();
        _fillWithCoordSumView = new FillWithCoordSumView();
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
}