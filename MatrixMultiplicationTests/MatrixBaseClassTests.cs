namespace MatrixMultiplicationTests;

public class MatrixBaseClassTests
{
    [Test]
    public void CanInitializeMatrixObject()
    {
        var matrix = new Matrix();
    }

    [Test]
    public void CanInitializeMatrixWithParameters()
    {
        var matrix = new Matrix(3, 3);
    }

    [Test]
    public void CanGetSizeOfMatrix3By3()
    {
        var matrix = new Matrix(3, 3);

        Assert.Multiple(() =>
        {
            Assert.That(matrix.Rows, Is.EqualTo(3));
            Assert.That(matrix.Columns, Is.EqualTo(3));
        });
    }

    [Test]
    public void CanGetSizeOfMatrix4By4()
    {
        var matrix = new Matrix(4, 4);

        Assert.Multiple(() =>
        {
            Assert.That(matrix.Rows, Is.EqualTo(4));
            Assert.That(matrix.Columns, Is.EqualTo(4));
        });
    }

    [Test]
    public void CanFillMatrixWithOneNumber()
    {
        var matrix = new Matrix(3, 3);

        matrix.FillWithNumber(0);
    }

    [Test]
    public void CanGetValuesOfMatrixFilledWithZeros()
    {
        var matrix = new Matrix(3, 3);

        matrix.FillWithNumber(0);

        var values = new[,]
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }
        };

        Assert.That(matrix.Values, Is.EqualTo(values));
    }

    [Test]
    public void CanGetValuesOfMatrixFilledWithOnes()
    {
        var matrix = new Matrix(3, 3);

        matrix.FillWithNumber(1);

        var values = new[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
        };

        Assert.That(matrix.Values, Is.EqualTo(values));
    }

    [Test]
    public void CanFillMatrixWithSumOfCoords()
    {
        var matrix = new Matrix(3, 3);

        matrix.FillWithSumOfCoords();
    }

    [Test]
    public void CanGetValuesOfFilledWithCoordsSumMatrix()
    {
        var matrix = new Matrix(3, 3);

        matrix.FillWithSumOfCoords();

        var values = new[,]
        {
            { 0, 1, 2 },
            { 1, 2, 3 },
            { 2, 3, 4 }
        };

        Assert.That(matrix.Values, Is.EqualTo(values));
    }

    [Test]
    public void CanFillMatrixWithProductOfCoords()
    {
        var matrix = new Matrix(3, 3);

        matrix.FillWithProductOfCoords();
    }

    [Test]
    public void CanGetValuesOfFilledWithCoordsProductMatrix()
    {
        var matrix = new Matrix(3, 3);

        matrix.FillWithProductOfCoords();

        var values = new[,]
        {
            { 0, 0, 0 },
            { 0, 1, 2 },
            { 0, 2, 4 }
        };

        Assert.That(matrix.Values, Is.EqualTo(values));
    }

    [Test]
    public void CanMultiplyTwoMatrices()
    {
        var firstMatrix = new Matrix();
        var secondMatrix = new Matrix();

        var result = firstMatrix * secondMatrix;
    }

    [Test]
    public void CanMultiplyTwoMatricesFilledWithZerosAndGetResult()
    {
        var firstMatrix = new Matrix(3, 3);
        var secondMatrix = new Matrix(3, 3);

        firstMatrix.FillWithNumber(0);
        secondMatrix.FillWithNumber(0);

        var result = firstMatrix * secondMatrix;

        var values = new[,]
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }
        };

        Assert.That(result.Values, Is.EqualTo(values));
    }

    [Test]
    public void CanMultiplyTwoMatricesFilledWithOnesAndGetResult()
    {
        var firstMatrix = new Matrix(3, 3);
        var secondMatrix = new Matrix(3, 3);

        firstMatrix.FillWithProductOfCoords();
        secondMatrix.FillWithProductOfCoords();

        var result = firstMatrix * secondMatrix;

        var values = new[,]
        {
            { 0, 0, 0 },
            { 0, 5, 10 },
            { 0, 10, 20 }
        };

        Assert.That(result.Values, Is.EqualTo(values));
    }

    [Test]
    public void CanGetDotProduct()
    {
        var array1 = new long[] { 1, 2, 3 };
        var array2 = new long[] { 1, 2, 3 };

        var dotProduct = Matrix.DotProduct(array1, array2);

        Assert.That(dotProduct, Is.EqualTo(14));
    }

    [Test]
    public void CanGetColumnFromTwoDimensionalArray()
    {
        var array = new long[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var col = Matrix.GetColumn(array, 1);

        var result = new int[] { 2, 5, 8 };

        Assert.That(col, Is.EqualTo(result));
    }

    [Test]
    public void CanGetRowFromTwoDimensionalArray()
    {
        var array = new long[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var col = Matrix.GetRow(array, 1);

        var result = new int[] { 4, 5, 6 };

        Assert.That(col, Is.EqualTo(result));
    }
}