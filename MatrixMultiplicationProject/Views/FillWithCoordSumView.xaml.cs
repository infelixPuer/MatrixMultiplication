using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationProject.Views;

public partial class FillWithCoordSumView
{
    public FillWithCoordSumView()
    {
        InitializeComponent();
        DataContext = new FillWithCoordSumViewModel();
    }
}