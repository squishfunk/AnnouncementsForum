using AnnouncementsForum.Models;

namespace AnnouncementsForum.Services.Interfaces
{
    public interface IForbiddenWordsService
    {
        List<ForbiddenWords> GetAll();
        ForbiddenWords GetById(int id);
        List<String> GetAllForbiddenWordsInString(string text);
    }
}
