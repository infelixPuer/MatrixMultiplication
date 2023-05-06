using MatrixMultiplicationProject.ViewModels;

namespace MatrixMultiplicationTests;

public class EnterDataViewModelTests
{
    [Test]
    public void CanInstantiateEnterDataViewModelObject()
    {
        var vm = new EnterDataViewModel();
    }

    [Test]
    public void CanSetSizeForMatrices()
    {
        var vm = new EnterDataViewModel
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
        var vm = new EnterDataViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };

        vm.ConfirmMatrixSizes();
    }

    [Test]
    public void CanGetMatrices()
    {
        var vm = new EnterDataViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };

        vm.ConfirmMatrixSizes();

        var firstMatrix = vm.FirstMatrix;
        var secondMatrix = vm.SecondMatrix;
    }

    [Test]
    public void AreMatrixSizesSetsCorrectly()
    {
        var vm = new EnterDataViewModel
        {
            FirstMatrixRows = 3,
            FirstMatrixColumns = 3,
            SecondMatrixRows = 3,
            SecondMatrixColumns = 3
        };

        vm.ConfirmMatrixSizes();

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
}