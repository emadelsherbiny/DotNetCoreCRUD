using System;
using System.Collections.Generic;
using System.Text;

namespace INNOVATEQ.DATA.DTO
{
    public class API_Result<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
