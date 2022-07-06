using System;
using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(4,resultado);

        }

        [Theory]
        [InlineData(1,2,3)]
        [InlineData(2,3,5)]
        [InlineData(3,4,7)]
        [InlineData(4,5,9)]
        public void Calculadora_Somar_EfeturarOperacaoSoma(double v1, double v2, double resultado)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultadoSoma = calculadora.Somar(v1, v2);

            // Assert
            Assert.Equal(resultado, resultadoSoma);

        }
    }
}
