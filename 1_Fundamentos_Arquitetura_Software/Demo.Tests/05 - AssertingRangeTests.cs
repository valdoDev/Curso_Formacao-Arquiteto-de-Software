using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertingRangeTests
    {
        [Theory]
        [InlineData(700)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(15000)]
        public void Funcionario_Salalrio_FaixasSalariaisDevemRespeitarNivelProfissional(double salario)
        {
            // Arrange && Act
            var funcionario = new Funcionario("Eduardo", salario);

            // Assert
            switch (funcionario.NivelProfissional)
            {
                case NivelProfissional.Junior:
                    Assert.InRange(funcionario.Salario, 500, 1999);
                    
                    break;
                case NivelProfissional.Pleno:
                    Assert.InRange(funcionario.Salario, 2000, 7999);
                    break;
                case NivelProfissional.Senior:
                    Assert.InRange(funcionario.Salario, 8000, double.MaxValue);
                    break;
                default:
                    Assert.NotInRange(funcionario.Salario, 0, 499);
                    break;
            }
        }
    }
}
