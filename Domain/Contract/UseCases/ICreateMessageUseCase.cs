using Domain.Entity;

namespace Domain.Contract.UseCases
{
    public interface ICreateMessageUseCase
    {
        Task<string> CreatePromptAsync(Event event_info);
        Task<string> CreateMessageAsync(string prompt);
        Task<bool> IsValidEventAsync(Event event_info);
        Task<bool> IsValidMessageAsync(string message);
        Task<bool> IsValidPromptAsync(string prompt);
    }
}
