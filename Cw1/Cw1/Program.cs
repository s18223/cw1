using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Cw1
{
    class Program
    {

        private static string emailRegex =
           @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";

        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentNullException();
            }
            else if (!isUrlValid(args[0]))
            {
                throw new ArgumentException();
            }

            var httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.GetAsync(args[0]);
            
            var body = response.Content.ReadAsStringAsync();
            var regex = new Regex(emailRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var emails = new HashSet<string>();
            foreach (Match match in regex.Matches(body.Result))
            {
                emails.Add(match.Value.ToString());
            }
            if (emails.Count == 0)
            {
                Console.WriteLine("Ńie znaleziono adresów email");
            }
            else
            {

                foreach (string email in emails)
                {
                    Console.WriteLine(email);
                }
            }
        }

        private static bool isUrlValid(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result)
                && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
