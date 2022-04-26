using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INNOVATEQ.API.Helper
{
    public class PaginationFilter
    {
        const int maxPageSize = 2000;

        public int PageNumber { get; set; } = 1;

        private int _PageSize { get; set; } = 10;

        public int PageSize
        {

            get { return _PageSize; }
            set
            {
                _PageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public bool Desc { get; set; } = true;
    }
}
