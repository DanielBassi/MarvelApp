namespace MarvelApp.Domain.Entities
{
    public abstract class Auditory
    {
        public Guid Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set;}
        public DateTime? Deleted_at { get; set; }
    }
}
