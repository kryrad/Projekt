using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab01
{
    class WikiConnection
    {
        static readonly string Url = "https://en.wikipedia.org/wiki/Special:Random";

        public static async Task<string> LoadDataAsync()
        {
            Task<string> result;
            string results;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(Url))
            using (HttpContent content = response.Content)
            {
                result = content.ReadAsStringAsync();
                results = await content.ReadAsStringAsync();
            }
            return await result;
        }
    }
}
