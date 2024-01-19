using AnnouncementsForum.Models;

namespace AnnouncementsForum.Services.Interfaces
{
    public interface IAnnoucmentsService
    {
        List<Announcement> GetAll();
        Announcement Get(int id);
        PaginAnnoucmentModel GetPagin(PaginAnnoucmentModel paginModel);
        string TryToFindRecord(List<SearchModel> searchText);
        class SearchModel
        {
            public List<string> And { get; set; } = new List<string>();
            public List<string> Not { get; set; } = new List<string>();
        };
        List<SearchModel> ParseSearchText(string text);
    }
}
