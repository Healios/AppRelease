using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
    public class Error : IAppReleaseResult
    {
        public Error(string message)
        {
            Message = message;
        }

        public Error(IEnumerable<string> messages)
        {
            Messages = messages;
        }

        public string Message { get; set; }
        public bool HasMultipleMessages { get => Messages.Count() > 0; }
        public IEnumerable<string> Messages { get; set; } = new List<string>();
    }
}
