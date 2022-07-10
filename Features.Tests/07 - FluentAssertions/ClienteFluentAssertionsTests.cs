using Features.Clientes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using Xunit.Abstractions;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    // Exemplo 1
    public class ClienteFluentAssertionsTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;
        private readonly ITestOutputHelper _outputHelper;

        public ClienteFluentAssertionsTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture,
                                             ITestOutputHelper outputHelper)
        {
            _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(1, "Deve possuir erros de validacao");

            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");
        }

    }
}
