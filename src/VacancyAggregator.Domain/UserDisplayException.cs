using System;

namespace VacancyAggregator.Domain
{
    [Serializable]
    public class UserDisplayException : Exception
    {
        public UserDisplayException(string message)
            : base(message)
        {
        }

        public UserDisplayException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
