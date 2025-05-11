namespace MarvelApp.Application.DTOs
{
    public class ComicDto
    {
        public bool isFavorite { get; set; }
        public int Id { get; set; }
        public int DigitalId { get; set; }
        public string Title { get; set; }
        public int IssueNumber { get; set; }
        public string VariantDescription { get; set; }
        public string Description { get; set; }
        public string Modified { get; set; }
        public string Isbn { get; set; }
        public string Upc { get; set; }
        public string DiamondCode { get; set; }
        public string Ean { get; set; }
        public string Issn { get; set; }
        public string Format { get; set; }
        public int PageCount { get; set; }
        public List<TextObject> TextObjects { get; set; }
        public string ResourceURI { get; set; }
        public List<Url> Urls { get; set; }
        public Series Series { get; set; }
        public List<Variant> Variants { get; set; }
        public List<object> Collections { get; set; }
        public List<object> CollectedIssues { get; set; }
        public List<DateItem> Dates { get; set; }
        public List<Price> Prices { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public List<object> Images { get; set; }
        public Creators Creators { get; set; }
        public Characters Characters { get; set; }
        public Stories Stories { get; set; }
        public Events Events { get; set; }
    }

    public class TextObject
    {
        public string Type { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
    }

    public class Url
    {
        public string Type { get; set; }
        public string UrlLink { get; set; }
    }

    public class Series
    {
        public string ResourceURI { get; set; }
        public string Name { get; set; }
    }

    public class Variant
    {
        public string ResourceURI { get; set; }
        public string Name { get; set; }
    }

    public class DateItem
    {
        public string Type { get; set; }
        public string Date { get; set; }
    }

    public class Price
    {
        public string Type { get; set; }
        public decimal PriceAmount { get; set; }
    }

    public class Thumbnail
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }

    public class Creators
    {
        public int Available { get; set; }
        public string CollectionURI { get; set; }
        public List<CreatorItem> Items { get; set; }
        public int Returned { get; set; }
    }

    public class CreatorItem
    {
        public string ResourceURI { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class Characters
    {
        public int Available { get; set; }
        public string CollectionURI { get; set; }
        public List<object> Items { get; set; }
        public int Returned { get; set; }
    }

    public class Stories
    {
        public int Available { get; set; }
        public string CollectionURI { get; set; }
        public List<StoryItem> Items { get; set; }
        public int Returned { get; set; }
    }

    public class StoryItem
    {
        public string ResourceURI { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class Events
    {
        public int Available { get; set; }
        public string CollectionURI { get; set; }
        public List<object> Items { get; set; }
        public int Returned { get; set; }
    }

}
