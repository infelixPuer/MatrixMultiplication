namespace MatrixMultiplicationTests;

public class MatrixMultiplicationBaseTests
{
    [Test]
    public void Can_fill_matrix_with_one_number()
    {
        var matrix = new long[3, 3];
        matrix.FillWithNumber(1);

        var result = new long[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
        };

        Assert.That(result, Is.EqualTo(matrix));
    }

    [Test]
    public void Can_fill_matrix_with_coords_sum()
    {
        var matrix = new long[3, 3];
        matrix.FillWithCoordsSum();

        var result = new long[,]
        {
            { 0, 1, 2 },
            { 1, 2, 3 },
            { 2, 3, 4 }
        };

        Assert.That(result, Is.EqualTo(matrix));
    }

    [Test]
    public void Can_fill_matrix_with_coords_product()
    {
        var matrix = new long[3, 3];
        matrix.FillWithCoordsProduct();

        var result = new long[,]
        {
            { 0, 0, 0 },
            { 0, 1, 2 },
            { 0, 2, 4 }
        };

        Assert.That(result, Is.EqualTo(matrix));
    }

    [Test]
    public void Can_multiply_two_matrices()
    {
        var matrix1 = new long[2, 3];
        var matrix2 = new long[3, 5];

        matrix1.FillWithNumber(1);
        matrix2.FillWithNumber(1);

        var result = MatrixMultiplicationBase.Multiply(matrix1, matrix2);

        var expected = new long[2, 5];
        expected.FillWithNumber(3);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Multiply_Cancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var tokenSource = new CancellationTokenSource();
        var matrix1 = new long[500, 500];
        var matrix2 = new long[500, 1000];
        matrix1.FillWithCoordsSum();
        matrix2.FillWithCoordsSum();


        // Act & Assert
        Assert.Throws<OperationCanceledException>(() =>
        {
            var task = Task.Run(() => MatrixMultiplicationBase.MultiplyAsync(matrix1, matrix2, tokenSource.Token), tokenSource.Token);
            tokenSource.Cancel();
            task.Wait(tokenSource.Token);
        });
    }

    [Test]
    public void Multiply_CorrectResult()
    {
        // Arrange
        var matrix1 = new long[,] { { 1, 2 }, { 3, 4 } };
        var matrix2 = new long[,] { { 5, 6 }, { 7, 8 } };

        // Act
        var result = MatrixMultiplicationBase.MultiplyAsync(matrix1, matrix2, CancellationToken.None);

        // Assert
        Assert.That(new long[,] { { 19, 22 }, { 43, 50 } }, Is.EqualTo(result.Result));
    }
}