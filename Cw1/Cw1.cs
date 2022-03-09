using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// can test on https://en.wikipedia.org/wiki/Email_address

namespace APBD
{
    public class Cw1
    {
        private static HttpClient httpClient = new(); // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        public static async Task Main(string[] args)
        {

            if (args.Length == 0)
            {
                throw new ArgumentNullException("REEE No URL here");
            }

            string url = args[0];

            if (!(Uri.IsWellFormedUriString(url, UriKind.Absolute)))
            {
                throw new ArgumentException("Why URL wrong. Please give URL good");
            }

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);


                if (response.IsSuccessStatusCode)
                {
                    string siteContent = await response.Content.ReadAsStringAsync();
                    String pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                    Regex regex = new(pattern, RegexOptions.IgnoreCase);
                    MatchCollection mathCollection = regex.Matches(siteContent);
                    HashSet<string> hashSet = new HashSet<string>();

                    foreach (Match match in mathCollection) {
                        hashSet.Add(match.Value); }

                    if (hashSet.Count >= 1)
                    {
                        foreach (string str in hashSet)
                            Console.WriteLine(str);
                    }
                    else
                    {
                        Console.WriteLine("No mail found :(");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Download error: " + e.Message);
            }
        }
    }
}
