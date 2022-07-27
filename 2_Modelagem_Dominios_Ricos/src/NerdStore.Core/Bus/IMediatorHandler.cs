using NerdStore.Core.Messages;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{
    public interface IMediatorHandler
    {
        /// <summary>
        /// O evento genérico "T" precisa ser filho de "Event". Ex.: DomainEvent que é filho de Event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="evento"></param>
        /// <returns></returns>
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
