using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKForm
{
    public interface IVKHttp
    {
        Task<string> getAsync(string uri);
        string urlEncode(string value);
    }
}
