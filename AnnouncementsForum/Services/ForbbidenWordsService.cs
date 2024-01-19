using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;

namespace AnnouncementsForum.Services
{
    public class ForbbidenWordsService : IForbiddenWordsService
    {
        private readonly DBContext _context;
        public ForbbidenWordsService(DBContext context)
        {
            _context = context;
        }

        public List<ForbiddenWords> GetAll()
        {
            return _context.ForbiddenWords.ToList();
        }
        public ForbiddenWords GetById(int id)
        {
            return _context.ForbiddenWords.Find(id);
        }
        public List<string> GetAllForbiddenWordsInString(string text)
        {
            var ForbiddenWords = this.GetAll();
            var AllForbiddenWordsUsed = new List<string>();
            foreach (var word in ForbiddenWords)
            {
                if(text.IndexOf(word.name) > -1)
                {
                    AllForbiddenWordsUsed.Add(word.name);
                }
            }
            return AllForbiddenWordsUsed;
        }
    }
}
