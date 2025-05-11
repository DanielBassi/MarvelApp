using System.Drawing;

namespace MarvelApp.Domain.ValueObjects
{
    public record Identification
    {
        private Identification() { }
        public string Value { get; }

        public Identification(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La identificación no puede estar vacío.", "Identification");

            if (!value.All(char.IsDigit) || value.Length != 10)
                throw new ArgumentException("La identificación no tiene un formato válido.", "Identification");

            Value = value;
        }
    }
}
