using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Features.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediator _mediator;

        public ClienteService(IClienteRepository clienteRepository, IMediator mediator)
        {
            _clienteRepository = clienteRepository;
            _mediator = mediator;
        }

        public IEnumerable<Cliente> ObterTodosAtivos()
        {
            return _clienteRepository.ObterTodos().Where(c => c.Ativo);
        }

        public void Adicionar(Cliente cliente)
        {
            if(!cliente.EhValido())
                return;

            _clienteRepository.Adicionar(cliente);
            _mediator.Publish(new ClienteEmailNotification("admin@gmail.com", cliente.Email, "Ola", "Bem vindo!"));
        }

        public void Atualizar(Cliente cliente)
        {
            if (!cliente.EhValido())
                return;

            _clienteRepository.Atualizar(cliente);
            _mediator.Publish(new ClienteEmailNotification("admin@gmail.com", cliente.Email, "Mudancas", "De uma olhada!"));

        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
        }

        public void Inativar(Cliente cliente)
        {
            if (!cliente.EhValido())
                return;

            cliente.Inativar();
            _clienteRepository.Atualizar(cliente);
            _mediator.Publish(new ClienteEmailNotification("admin@gmail.com", cliente.Email, "Inativo", "De uma olhada!"));

        }



        public void Remover(Cliente cliente)
        {
            _clienteRepository.Remover(cliente.Id);
            _mediator.Publish(new ClienteEmailNotification("admin@gmail.com", cliente.Email, "Removido", "De uma olhada!"));

        }
    }
}
