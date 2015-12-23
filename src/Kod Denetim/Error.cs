using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    class Error
    {
        public int LineNumber { get; set; }
        public string Message { get; set; }

        public Error()
        {

        }

        public Error(int lineNumber, string message)
        {
            LineNumber = lineNumber;
            Message = message;
        }
    }
}
