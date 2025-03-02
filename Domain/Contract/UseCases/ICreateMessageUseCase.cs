using Domain.Entity;

namespace Domain.Contract.UseCases
{
    public interface ICreateMessageUseCase
    {
        Task<string> CreateQuery(Event event_info);
        Task<string> CreateMessage(string query);
        Task<bool> IsValidQuery(string query);
        Task<bool> IsValidEvent(Event event_info);
        Task<bool> IsValidMessage(string message, string query);
    }
}
