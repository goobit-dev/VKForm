using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKForm
{
    class VkData
    {
        private List<string> countries;

        private IVKHttp client;

        public List<string> getCountries()
        {
            if (countries == null)
            {
                var list = new List<string>();

                // todo:

                countries = list;
            }
            return countries;            
        }
    }
}
