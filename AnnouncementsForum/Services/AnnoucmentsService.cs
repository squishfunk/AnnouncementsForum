using AnnouncementsForum.Services.Interfaces;
using AnnouncementsForum.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace AnnouncementsForum.Services
{
    public class AnnoucmentsService : IAnnoucmentsService
    {
        private readonly DBContext _context;
        public AnnoucmentsService(DBContext context)
        {
            _context = context;
        }
        public List<Announcement> GetAll()
        {
            return _context.Announcements.ToList();
        }
        public Announcement Get(int id)
        {
            return _context.Announcements.Find(id);
        }
        public PaginAnnoucmentModel GetPagin(PaginAnnoucmentModel paginModel)
        {
            if(paginModel.SearchString != String.Empty)
            {
                var parseText = ParseSearchText(paginModel.SearchString);
                var whereClausule = TryToFindRecord(parseText);
                paginModel.Data = _context.Announcements.FromSqlRaw($"SELECT * FROM `announcements` {whereClausule} ORDER BY CreateDate DESC").Skip(paginModel.Size * paginModel.Page).Take(paginModel.Size).ToList();
                paginModel.Page = paginModel.Page + 1;
                paginModel.Total = _context.Announcements.Count() / 10;
                return paginModel;
            }
            paginModel.Data = _context.Announcements.FromSql($"SELECT * FROM `announcements` ORDER BY CreateDate DESC").Skip(paginModel.Size * paginModel.Page).Take(paginModel.Size).ToList();
            paginModel.Page = paginModel.Page + 1;
            paginModel.Total = _context.Announcements.Count() / 10;
            return paginModel;
        }
        public List<IAnnoucmentsService.SearchModel> ParseSearchText(string text)
        {
            var splitOrInText = text.Split("|");
            List<IAnnoucmentsService.SearchModel> searchModels = new List<IAnnoucmentsService.SearchModel>();

            foreach(var item in splitOrInText)
            {
                IAnnoucmentsService.SearchModel searchModel = new IAnnoucmentsService.SearchModel();
                var splitAndInText = item.Split("&");
                foreach(var andString  in splitAndInText)
                {
                    var indexOfNotFlag = andString.IndexOf("!");
                    if (indexOfNotFlag != -1)
                    {
                        var splitNotInText = item.Split("!");
                        searchModel.Not.Add(splitNotInText[1]);
                        andString.Remove(indexOfNotFlag, splitNotInText[1].Count() + 1);
                    }
                    searchModel.And.Add(andString);
                }
                searchModels.Add(searchModel);
            }
            return searchModels;
        }
        public class SearchModel
        {
            public List<string> And { get; set; } = new List<string>();
            public List<string> Not { get; set; } = new List<string>();
        }
        public string TryToFindRecord(List<IAnnoucmentsService.SearchModel> searchText)
        {
            string isTextPass = " WHERE ";
            var stringsToAdd = new List<string>();
            foreach(var item in searchText)
            {
                var andIncludes = new List<string>();
                var notIncludes = new List<string>();
                foreach (var and in item.And)
                {
                    andIncludes.Add(" Description LIKE '%" + and.Trim() + "%' ");
                }
                foreach (var not in item.Not)
                {
                    notIncludes.Add(" Description NOT LIKE '%" + not.Trim() + "%' ");
                }
                var arrayStrings = new List<string>();
                if (andIncludes.Count() > 0) { arrayStrings.Add(string.Join(" AND ", andIncludes)); }
                if (notIncludes.Count() > 0) { arrayStrings.Add(string.Join(" AND ", notIncludes)); }
                stringsToAdd.Add(string.Join(" AND ", arrayStrings));
            }
            isTextPass = isTextPass + string.Join(" OR ", stringsToAdd);
            return isTextPass;
        }
    }
}
