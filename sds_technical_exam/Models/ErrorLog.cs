using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sds_technical_exam.Models
{
    public class ErrorLog
    {
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError { get { return ErrorMessage != ""; } }
        public bool IsException { get { return Exception != null; } }

        public ErrorLog()
        {
            Exception = null;
            ErrorMessage = "";
        }
    }
}