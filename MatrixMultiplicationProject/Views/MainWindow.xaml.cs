using System.Windows;

namespace MatrixMultiplicationProject.Views;

public partial class MainWindow
{
    private object _fillWithNumberView;

    public MainWindow()
    {
        InitializeComponent();
        _fillWithNumberView = new FillWithNumberView();
    }

    private void FillWithNumberButton_OnClick(object sender, RoutedEventArgs e)
    {
        ContentControl.Content = _fillWithNumberView;
    }
}