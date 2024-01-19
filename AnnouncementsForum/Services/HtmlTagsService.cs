using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;

namespace AnnouncementsForum.Services
{
    public class HtmlTagsService : IHtmlTagsService
    {
        private readonly DBContext _context;
        public HtmlTagsService(DBContext context)
        {
            _context = context;
        }
        public List<HtmlTags> GetAll()
        {
            return _context.HtmlTags.ToList();
        }
        public List<string> GetListOfForbiddenTags(string text)
        {
            var ForbiddenTags = this.GetAll();
            var AllForbiddenTagsUsed = new List<string>();
            foreach (var tag in ForbiddenTags)
            {
                if (tag.IsAllowed == false && (text.IndexOf("<" + tag.Name + ">") > -1 || text.IndexOf("</" + tag.Name + ">") > -1))
                {
                    AllForbiddenTagsUsed.Add(tag.Name);
                }
            }
            return AllForbiddenTagsUsed;
        }
    }
}
