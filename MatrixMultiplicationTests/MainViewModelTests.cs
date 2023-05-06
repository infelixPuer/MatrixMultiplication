using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationTests;

public class MainViewModelTests
{
    [Test]
    public void CanInstantiateEnterDataViewModelObject()
    {
        var vm = new MainViewModel();
    }

    [Test]
    public void CanSetSizeForMatrices()
    {
        var vm = new MainViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };
    }

    [Test]
    public void CanConfirmMatrixSizes()
    {
        var vm = new MainViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };

        vm.ConfirmMatrixSizesCommand.Execute(vm);
    }

    [Test]
    public void CanGetMatrices()
    {
        var vm = new MainViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };

        vm.ConfirmMatrixSizesCommand.Execute(vm);

        var firstMatrix = vm.FirstMatrix;
        var secondMatrix = vm.SecondMatrix;
    }

    [Test]
    public void AreMatrixSizesSetsCorrectly()
    {
        var vm = new MainViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };

        vm.ConfirmMatrixSizesCommand.Execute(vm);

        var firstMatrix = vm.FirstMatrix;
        var secondMatrix = vm.SecondMatrix;

        Assert.Multiple(() =>
        {
            Assert.That(firstMatrix?.GetLength(0), Is.EqualTo(vm.FirstMatrixRows));
            Assert.That(firstMatrix?.GetLength(1), Is.EqualTo(vm.FirstMatrixColumns));
            Assert.That(secondMatrix?.GetLength(0), Is.EqualTo(vm.SecondMatrixRows));
            Assert.That(secondMatrix?.GetLength(1), Is.EqualTo(vm.SecondMatrixColumns));
        });
    }

    [Test]
    public void EnterDataViewModel_MultiplyMatricesAsync()
    {
        var vm = new MainViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };
        vm.ConfirmMatrixSizesCommand.Execute(vm);

        vm.MultiplyMatricesAsyncCommand.Execute(vm);
    }

    [Test]
    public void EnterDataViewModel_MultiplyMatricesAsync_CorrectResult()
    {
        var vm = new MainViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };
        vm.ConfirmMatrixSizesCommand.Execute(vm);
        vm.FirstMatrix.FillWithNumber(1);
        vm.SecondMatrix.FillWithNumber(1);


        vm.MultiplyMatricesAsyncCommand.Execute(vm);
        

        Assert.That(new long[,] { { 3, 3, 3 }, { 3, 3, 3 }, { 3, 3, 3 } }, Is.EqualTo(vm.Result));
    }
}