﻿
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Bus;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    /// <summary>
    /// EstoqueService é um serviço de domínio
    /// </summary>
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _bus;

        public EstoqueService(IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _bus = mediatorHandler;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (produto.QuantidadeEstoque < 10)
            {
                await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }


        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();

        }


        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }

}
