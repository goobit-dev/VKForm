using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VKForm
{
    public class VkApi
    {
        public IVKHttp Client { get; private set; }

        public VkApi(IVKHttp client)
        {
            this.Client = client;
        }

        Dictionary<int, string> ParseXmlResponse(string response, string ElementName, string idName, string valueName)
        {
            XDocument xml = XDocument.Parse(response);

            var query = from country in xml.Root.Elements(ElementName)
                        select country;

            var list = new Dictionary<int, string>();
            foreach (XElement country in query)
            {
                list.Add(int.Parse(country.Element(idName).Value), country.Element(valueName).Value);
            }

            return list;
        }

        public async Task<Dictionary<int, string>> getCountriesAsync()
        {
            string response = await Client.getAsync("https://api.vk.com/method/database.getCountries.xml");
            return ParseXmlResponse(response, "country", "cid", "title");
        }

        public async Task<Dictionary<int, string>> getCitiesAsync(int countryId, string query)
        {
            string uri = string.Format("https://api.vk.com/method/database.getCities.xml?country_id={0}", countryId);

            if (!string.IsNullOrEmpty(query))
            {
                uri = string.Format("{0}&q={1}", uri, Client.urlEncode(query));
            }

            string response = await Client.getAsync(uri);
            return ParseXmlResponse(response, "city", "cid", "title");
        }

        public async Task<Dictionary<int, string>> getUniveritiesAsync(int countryId, int cityId, string query)
        {
            string uri = string.Format("https://api.vk.com/method/database.getUniversities.xml?country_id={0}", countryId);

            if (cityId != 0)
            {
                uri = string.Format("{0}&city_id={1}", uri, cityId);
            }

            if (!string.IsNullOrEmpty(query))
            {
                uri = string.Format("{0}&q={1}", uri, Client.urlEncode(query));
            }

            string response = await Client.getAsync(uri);
            return ParseXmlResponse(response, "university", "id", "title");
        }
    }
}
