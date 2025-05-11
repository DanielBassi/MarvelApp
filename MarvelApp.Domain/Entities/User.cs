using MarvelApp.Domain.ValueObjects;

namespace MarvelApp.Domain.Entities
{
    public class User : Auditory
    {
        public string Name { get; set; } = null!;
        public Email Email { get; set; } = null!;
        public Identification Identification { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
