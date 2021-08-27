using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Portfolio.Common
{
    public class BadRequestResponse: ApiResponse
    {
        public BadRequestResponse()
            : base(400, HttpStatusCode.BadRequest.ToString())
        {
        }


        public BadRequestResponse(string message)
            : base(400, HttpStatusCode.BadRequest.ToString(), message)
        {
        }
    }
}
