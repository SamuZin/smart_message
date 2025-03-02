using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract.UseCases
{
    public interface ILhama
    {
        Task<string> GenerateResponseAsync(string prompt, string terms);
        Task<bool> ValidateResponseContextAsync(string response, string prompt);
        Task<bool> ValidateResponseTermsAsync(string response, string terms);
        // Task<double> EvaluateMessageQualityAsync(string message, List<string> criteria);
    }
}
