using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;

namespace AnnouncementsForum.Services
{
    public class AdminAnnoucmentsService : IAdminAnnoucmentsService
    {
        private readonly DBContext _context;
        public AdminAnnoucmentsService(DBContext context) 
        {
            _context = context;
        }

        public List<AdminAnnoucments> GetAll()
        {
            return _context.AdminAnnoucments.ToList();
        }
        public void Create(AdminAnnoucments annoucment)
        {
            _context.AdminAnnoucments.Add(annoucment);
            _context.SaveChanges();
        }
        public void Delete(AdminAnnoucments annoucment)
        {
            _context.AdminAnnoucments.Remove(annoucment);
            _context.SaveChanges();
        }
    }
}
