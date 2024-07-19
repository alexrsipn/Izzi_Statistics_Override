using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izzi_Statistics_Override_WPF.Model
{
    public class OFSC_Response
    {
        public int statusCode { get; set; }
        public string content { get; set; }
        public string errorMessage { get; set; }
    }
    public class Response_Content
    {
        public int status { get; set; }
        public string content { get; set; }
    }
}
