using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Portfolio.Common
{
    public class NotFoundResponse : ApiResponse
    {
        public NotFoundResponse()
            : base(404, HttpStatusCode.NotFound.ToString())
        {
        }


        public NotFoundResponse(string message)
            : base(404, HttpStatusCode.NotFound.ToString(), message)
        {
        }
    }
}
