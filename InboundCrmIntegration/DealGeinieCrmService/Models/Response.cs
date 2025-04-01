using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DealGeinieCrmService.Models
{
    public class Response
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string Message { get; set; }
    }
}