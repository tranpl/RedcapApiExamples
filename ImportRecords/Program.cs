using Newtonsoft.Json;

using Redcap;
using Redcap.Models;

namespace ImportRecords
{
    internal class Demographics
    {
        [JsonRequired]
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
            ImportData();
            Console.ReadLine();

        }
        public static List<Demographics> GetDemographics()
        {
            var demographics = new List<Demographics>
            {
                new Demographics
                {
                    RecordId = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "123 Main St"
                },
                new Demographics
                {
                    RecordId = "2",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Address = "456 Elm St"
                }
            };
            return demographics;
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
        public static async void ImportData()
        {
            Console.WriteLine("Importing data...");

            var redcapOptions = GetRedcapApiOptions();
            var demographicsDataToImport = GetDemographics();

            var redcapApi = new RedcapApi(redcapOptions.ApiUrl);
            var result = await redcapApi
                .ImportRecordsAsync(
                redcapOptions.Token,
                Content.Record,
                RedcapFormat.json,
                RedcapDataType.flat,
                OverwriteBehavior.normal,
                forceAutoNumber: true,
                backgroundProcess: true,
                data: demographicsDataToImport,
                dateFormat: "MDY",
                CsvDelimiter.comma,
                ReturnContent.count,
                RedcapReturnFormat.json
            );
            Console.WriteLine("Data Import completed...");
            Console.WriteLine("Import results: " + result);
        }
    }

    internal class RedcapApiOptions
    {
        public string ApiUrl { get; set; } = "http://localhost/api/";
        public string Token { get; set; } = "your-api-token-here";

    }
}
