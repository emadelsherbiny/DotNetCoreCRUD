using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INNOVATEQ.API.Helper
{
    public class PagedStatistics<T>
    {
        public int CurrentPage { get;   set; }
        public int TotalPages { get;   set; }
        public int PageSize { get;   set; } 
        public int TotalRecords { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public  PagedList< T>  Data { get; set; }

    }
}
