using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INNOVATEQ.API.Helper
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            IsSuccess = true;
            Message = string.Empty;

            Data = data;
        }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
