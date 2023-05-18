using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationProject.Views;

public partial class FillWithRandomNumbersView
{
    public FillWithRandomNumbersView()
    {
        InitializeComponent();
        DataContext = new FillWithRandomNumbersViewModel();
    }
}