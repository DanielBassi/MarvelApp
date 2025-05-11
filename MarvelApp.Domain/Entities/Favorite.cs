namespace MarvelApp.Domain.Entities
{
    public class Favorite : Auditory
    {
        public Guid UserId { get; set; }
        public int ComicId { get; set; }
    }
}
