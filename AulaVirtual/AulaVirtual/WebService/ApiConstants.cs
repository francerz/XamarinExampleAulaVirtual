using System;
using System.Collections.Generic;
using System.Text;

namespace AulaVirtual.WebService
{
    public class ApiConstants
    {
#if WINDOWS_UWP
        public const string API_URL = "http://localhost:3000";
#else
        public const string API_URL = "http://192.168.1.100:3000";
#endif
    }
}
