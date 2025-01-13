using Newtonsoft.Json;

using Redcap;
using Redcap.Models;

namespace ExportRecords
{
    internal class Demographics
    {
        [JsonProperty("record_id")]
        public string RecordId { get; set; }
        [JsonProperty("first_name")]

        public string FirstName { get; set; }
        [JsonProperty("last_name")]

        public string LastName { get; set; }
        [JsonProperty("address")]

        public string Address { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = ExportData().Result;
            Console.WriteLine(data);
            Console.ReadLine();

        }
        public static RedcapApiOptions GetRedcapApiOptions()
        {
            var options = new RedcapApiOptions
            {
                ApiUrl = "http://localhost/api/",
                Token = "0DC56E971C71121DF6F6264B93D64EAA"
            };
            return options;
        }
        public static async Task<string> ExportData()
        {
            Console.WriteLine("Exporting data...");

            var token = "0DC56E971C71121DF6F6264B93D64EAA";
            var apiUrl = "http://localhost/api/";

            var redcapApi = new RedcapApi(apiUrl);
            var data = await redcapApi
                .ExportRecordsAsync(
                token,
                RedcapFormat.json,
                RedcapDataType.flat
            );
            Console.WriteLine("Data export completed...");
            var result = data;
            var demographics = JsonConvert.DeserializeObject<List<Demographics>>(result);
            var jsonString = JsonConvert.SerializeObject(demographics, Formatting.Indented);
            return jsonString;
        }
    }

    internal class RedcapApiOptions
    {
        public string ApiUrl { get; set; } = "http://localhost/api/";
        public string Token { get; set; } = "your-api-token-here";

    }
}
