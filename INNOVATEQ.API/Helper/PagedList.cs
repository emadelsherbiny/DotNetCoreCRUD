using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INNOVATEQ.API.Helper
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static PagedStatistics<T> ToPagedListResult(IQueryable<T> source, int pageNumber, int pageSize,string Message="")
        {
            PagedStatistics<T> result = new PagedStatistics<T>()
            {   IsSuccess=true,
                Message = Message,
                CurrentPage = pageNumber,
                PageSize= pageSize,
                TotalRecords=source.Count(),
                TotalPages= (int)Math.Ceiling(source.Count() / (double)pageSize),
                Data= new PagedList<T>(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), source.Count(), pageNumber, pageSize)
            };
            return result;
        }
      
    }
}
