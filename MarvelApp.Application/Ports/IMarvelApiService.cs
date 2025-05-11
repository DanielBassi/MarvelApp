using MarvelApp.Application.DTOs;

namespace MarvelApp.Application.Ports
{
    public interface IMarvelApiService
    {
        Task<List<ComicDto>?> GetListAsync();
        Task<ComicDto?> GetOneAsync(int comicId);
    }
}
