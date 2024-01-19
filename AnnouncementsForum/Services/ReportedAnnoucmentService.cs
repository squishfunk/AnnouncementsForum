using Microsoft.EntityFrameworkCore;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;

namespace AnnouncementsForum.Services
{
    public class ReportedAnnoucmentService : IReportedAnnoucment
    {
        private readonly DBContext _context;
        public ReportedAnnoucmentService(DBContext context)
        {
            _context = context;
        }
        public List<ReportedAnnoucmentModel> GetAll()
        {
            var data = (from ra in _context.ReportedAnnoucments
                        join a in _context.Announcements
                        on ra.AnnouncementId equals a.Id
                        join u in _context.Users
                        on ra.UserId equals u.Id
                        select new ReportedAnnoucmentModel
                        {
                            ID = ra.Id,
                            UserName = u.UserName,
                            Description = ra.Description,
                            AnnoucmentTitle = a.Title,
                            AnnoucmentDescription = a.Description,
                            AnnoucmentId = a.Id
                        })
              .ToList();
            return data;
        }
        public void RemovePost(ReportedAnnoucmentModel reportedAnnoucmentModel)
        {
            Announcement announcement = new Announcement();
            ReportedAnnoucment reportedAnnoucmentDelete = new ReportedAnnoucment();
            announcement.Id = reportedAnnoucmentModel.AnnoucmentId;
            reportedAnnoucmentDelete.Id = reportedAnnoucmentModel.ID;

            _context.ReportedAnnoucments.Remove(reportedAnnoucmentDelete);
            _context.SaveChanges();
            _context.Announcements.Remove(announcement);
            _context.SaveChanges();
        }
        public void RemoveReport(ReportedAnnoucmentModel reportedAnnoucmentModel)
        {
            ReportedAnnoucment reportedAnnoucmentDelete = new ReportedAnnoucment();
            reportedAnnoucmentDelete.Id = reportedAnnoucmentModel.ID;

            _context.ReportedAnnoucments.Remove(reportedAnnoucmentDelete);
            _context.SaveChanges();
        }
    }
}
