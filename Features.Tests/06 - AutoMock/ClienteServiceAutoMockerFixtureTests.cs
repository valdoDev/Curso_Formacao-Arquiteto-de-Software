using Features.Clientes;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    // Segundo exemplo, copia de ClienteServiceAutoMockerTests, neste caso utilzando com Fixture
    public class ClienteServiceAutoMockerFixtureTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerBogus;

        public ClienteServiceAutoMockerFixtureTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerBogus)
        {
            _clienteTestsAutoMockerBogus = clienteTestsAutoMockerBogus;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerBogus.GerarClienteValido();
            var clienteService = _clienteTestsAutoMockerBogus.ObterClienteservice();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            _clienteTestsAutoMockerBogus.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestsAutoMockerBogus.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);

        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerBogus.GerarClienteInvalido();
            var clienteService = _clienteTestsAutoMockerBogus.ObterClienteservice();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            _clienteTestsAutoMockerBogus.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestsAutoMockerBogus.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);

        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var clienteService = _clienteTestsAutoMockerBogus.ObterClienteservice();

            _clienteTestsAutoMockerBogus.Mocker.GetMock<IClienteRepository>()
                  .Setup(c => c.ObterTodos())
                  .Returns(_clienteTestsAutoMockerBogus.ObterClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert 
            _clienteTestsAutoMockerBogus.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);

        }
    }
}
