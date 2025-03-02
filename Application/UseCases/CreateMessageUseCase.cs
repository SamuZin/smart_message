using Domain.Contract.UseCases;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateMessageUseCase : ICreateMessageUseCase
    {
        private readonly ILhama _lhama;
        private string terms;
        public CreateMessageUseCase(ILhama lhama)
        {
            _lhama = lhama;
            terms = File.ReadAllText("terms.txt");

        }

        public async Task<string> CreateMessageAsync(string prompt)
        {
            string message = await _lhama.GenerateResponseAsync(prompt, terms);

            if (string.IsNullOrEmpty(message))
                throw new Exception();
            
            if (!await _lhama.ValidateResponseContextAsync(message, prompt))
                throw new Exception();

            if (!await _lhama.ValidateResponseTermsAsync(message, terms))
                throw new Exception();

            return message;
        }

        public async Task<string> CreatePromptAsync(Event event_info)
        {
            string initial_prompt = $"Create a prompt to generate a personalized message for the following event: \n" +
                           $"Event Name: {event_info.EventName}. \n" +
                           $"Event Description: {event_info.EventDescription}. \n" +
                           $"Event Date: {event_info.Date}. \n" +
                           $"The event description asks for the message to talk about [include important aspects or requested actions from the description]. \n" +
                           $"Use the context from the description to create a message that is aligned with the event and communicates the right message clearly and appropriately.";

            string main_prompt = await _lhama.GenerateResponseAsync(initial_prompt, terms);

            if (string.IsNullOrEmpty(main_prompt))
                throw new Exception();

            if (!await _lhama.ValidateResponseContextAsync(main_prompt, initial_prompt))
                throw new Exception();

            if (!await _lhama.ValidateResponseTermsAsync(main_prompt, terms))
                throw new Exception();

            return main_prompt;
        }

        public async Task<bool> IsValidEventAsync(Event event_info)
        {
            // Check if event_info is null
            if (event_info == null)
                return false;

            // Check if event name is null
            if (string.IsNullOrEmpty(event_info.EventName))
                return false;

            // Check if event name is less than 3 characters
            if (event_info.EventName.Length <= 3)
                return false;

            // Check if event name contains forbidden keywords (SQL Injection)
            if (await ContainsForbiddenKeywirds(event_info.EventName))
                return false;

            // Check if event description is null
            if (string.IsNullOrEmpty(event_info.EventDescription))
                return false;

            // Check if event description is less than 3 characters
            if (event_info.EventDescription.Length <= 3)
                return false;

            // Check if event description contains forbidden keywords (SQL Injection)
            if (await ContainsForbiddenKeywirds(event_info.EventDescription))
                return false;

            return true;
        }

        public async Task<bool> IsValidMessageAsync(string message)
        {
            if (string.IsNullOrEmpty(message))
                return false;

            if (await ContainsForbiddenKeywirds(message))
                return false;

            return true;
        }

        public async Task<bool> IsValidPromptAsync(string prompt)
        {
            // Check if prompt is null
            if (string.IsNullOrEmpty(prompt))
                return false;

            // Check if prompt is less than 3 characters or more than 500 characters
            if (prompt.Length <= 3 || prompt.Length > 500)
                return false;

            // Check if prompt contains forbidden keywords (SQL Injection)
            if (await ContainsForbiddenKeywirds(prompt))
                return false;

            return true;
        }

        private Task<bool> ContainsForbiddenKeywirds(string text)
        {
            string[] forbiddenKeywords = { "DROP", "DELETE", "INSERT", "UPDATE", "--", ";", "<script>", "</script>" };
            for (int i = 0; i < forbiddenKeywords.Length; i++)
            {
                if (text.Contains(forbiddenKeywords[i]))
                    return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
