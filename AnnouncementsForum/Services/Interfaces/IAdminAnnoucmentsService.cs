using AnnouncementsForum.Models;

namespace AnnouncementsForum.Services.Interfaces
{
    public interface IAdminAnnoucmentsService
    {
        List<AdminAnnoucments> GetAll();
        void Create(AdminAnnoucments annoucment);
        void Delete(AdminAnnoucments annoucment);
    }
}
