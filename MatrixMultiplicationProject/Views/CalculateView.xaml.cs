using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationProject.Views;

public partial class CalculateView
{
    public CalculateView()
    {
        InitializeComponent();
        DataContext = new CalculateViewModel();
    }
}