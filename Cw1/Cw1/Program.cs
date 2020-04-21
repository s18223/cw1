using System;
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
            var httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.GetAsync(args[0]);

            var body = response.Content.ReadAsStringAsync();
           
            var regex = new Regex(emailRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Match match in regex.Matches(body.Result))
            {
                Console.WriteLine(match.Value.ToString());
            }
        }
    }
}
