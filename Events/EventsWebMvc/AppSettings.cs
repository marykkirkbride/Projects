using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebMvc
{
    public class AppSettings
    {

        public string EventCatalogUrl { get; set; }
        //public string CartUrl { get; set; }
        public Logging Logging { get; set; }
    }



    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }
}
