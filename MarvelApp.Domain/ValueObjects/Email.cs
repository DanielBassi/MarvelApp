using System.Text.RegularExpressions;

namespace MarvelApp.Domain.ValueObjects
{
    public record Email
    {
        private Email() { }
        public string Value { get; }

        public Email(string value)
        {
            if (!IsValid(value))
                throw new ArgumentException("Email no válido", "Email");
            Value = value;
        }

        private static bool IsValid(string value) =>
            Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
