using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationProject.Views;

public partial class FillWithCoordsProductView
{
    public FillWithCoordsProductView()
    {
        InitializeComponent();
        DataContext = new FillWithCoordProductViewModel();
    }
}