using System.Windows.Controls;
using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationProject.Views;

public partial class FillWithNumberView : UserControl
{
    public FillWithNumberView()
    {
        InitializeComponent();
        DataContext = new FillWithNumberViewModel();
    }
}