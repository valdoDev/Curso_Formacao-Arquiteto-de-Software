using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertNumbersTest
    {
        [Fact]
        public void Calculadora_Somar_DeveSerIgual()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Somar(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Calculadora_Somar_NaoDeveSerIgual()
        {
            // Arrange
            var calculadora = new Calculadora();


            // Act
            var result = calculadora.Somar(1.1312, 2.2312);

            // Assert
            Assert.NotEqual(3.3, result, 1);

        }


    }
}
