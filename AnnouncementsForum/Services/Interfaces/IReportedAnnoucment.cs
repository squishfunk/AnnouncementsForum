using AnnouncementsForum.Models;

namespace AnnouncementsForum.Services.Interfaces
{
    public interface IReportedAnnoucment
    {
        List<ReportedAnnoucmentModel> GetAll();
        void RemovePost(ReportedAnnoucmentModel reportedAnnoucment);
        void RemoveReport(ReportedAnnoucmentModel reportedAnnoucment);
    }
}
