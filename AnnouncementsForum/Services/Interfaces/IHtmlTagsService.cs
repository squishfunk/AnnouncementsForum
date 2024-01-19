using AnnouncementsForum.Models;

namespace AnnouncementsForum.Services.Interfaces
{
    public interface IHtmlTagsService
    {
        List<HtmlTags> GetAll();
        List<string> GetListOfForbiddenTags(string text);
    }
}
