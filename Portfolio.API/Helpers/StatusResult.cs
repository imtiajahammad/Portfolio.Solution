using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Helpers
{
    public class StatusResult<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public StatusResult()
        {
            Status = ResponseStatus.Failed;
            Message = String.Empty;
        }
    }
    public enum ResponseStatus
    {
        Failed = 0,
        Success = 1,
        NotFound = 2,
        LoginSuccess=3,
        FetchSuccess=4
    }
}
