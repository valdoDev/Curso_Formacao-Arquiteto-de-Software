using Xunit;

namespace Demo.Tests
{
    public class AssertingCollectionsTests
    {

        [Fact]
        public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.All(funcionario.Habilidades, habilidade => Assert.False(string.IsNullOrEmpty(habilidade)));
        }

        [Fact]
        public void Funcionario_Habilidades_JuniorDevePossuirHabilidadeBasica()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            // Assert
            Assert.Contains("OOP", funcionario.Habilidades);

        }

        [Fact]
        public void Funcionario_habilidades_JuniorNaoDevePossuirHabilidadeAvancada()
        {
            // Arrange &  Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            // Assert
            Assert.DoesNotContain("Tests", funcionario.Habilidades);

        }

        [Fact]
        public void Funcionario_Habilidades_SeniorDevePossuirTodasHhabiliddades()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 15000);

            var habilidadesBasicas = new[]
            {
                "Logica de Programacao",
                "OOP",
                "Testes",
                "Microservices"
            };

            // Assert
            Assert.Equal(habilidadesBasicas, funcionario.Habilidades);

        }

    }
}
