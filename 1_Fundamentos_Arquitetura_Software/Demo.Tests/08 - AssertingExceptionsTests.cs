﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertingExceptionsTest
    {

        [Fact]
        public void Calculadora_Dividir_DeveRetornarErroDivisaoPorZero() {

            // Arrange
            var calculadora = new Calculadora();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(20, 0));
        }

        [Fact]
        public void Funcionario_Salario_DeveretornarErroSalarioInferiorPpermitido()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<Exception>(
                () => FuncionarioFactory.Criar("Eduardo", 300)
                );

            Assert.Equal("Salario inferior ao permitido!", exception.Message);

        }
    }
}
