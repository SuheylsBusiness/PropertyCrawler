using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Web;

namespace PropertyCrawler
{
    internal class Algorithm
    {
        static string _environmentPath = "";
        static void Main(string[] args)
        {
            #region Init environment
            Console.WriteLine($"({DateTime.Now.ToString("HH:mm")}): PropertyCrawler started.");
            if (new DirectoryInfo(Environment.CurrentDirectory).GetDirectories().Where(x => x.Name == "Misc").Count() > 0) _environmentPath = Environment.CurrentDirectory;
            else _environmentPath = Path.GetFullPath(Path.Combine(@$"{Environment.CurrentDirectory}", @"..\..\..\..\PropertyCrawler"));
            var locationList = File.ReadAllLines($@"{_environmentPath}\Misc\locationList.txt").ToList();
            var allResultObjs = new List<ResultObj>();
            var outputFileName = DateTime.Now.ToString("yyyy-MM-dd_hh-mm") + ".json";
            #endregion

            #region Scraping Algorithm
            //looping through location list
            for (int i = 0; i < locationList.Count; i++)
            {
                Console.WriteLine($"({DateTime.Now.ToString("HH:mm")}): Looping through location {i + 1}/{locationList.Count}.");
                try
                {
                    //api call to get geopath for input location
                    var geoPath = JsonConvert.DeserializeObject<dynamic>(GetResponse(linkToVisit: $"https://api.mobile.immobilienscout24.de/geo/autocomplete?nextgen=true&i={locationList[i]}", headerType: "ImmoScout24", isPostRequest: false, body: ""))["results"][0]["geopath"].ToString();

                    //crawling through all pages in the search results
                    for (int b = 0, page = 1; true; b++, page++)
                    {
                        Console.WriteLine($"({DateTime.Now.ToString("HH:mm")}): Looping through page {page}.");

                        //api call to retrieve search result records
                        var responseString = GetResponse(linkToVisit: $"https://api.mobile.immobilienscout24.de/search?searchType=region&realestatetype=apartmentrent&features=adKeysAndStringValues%2CcalculatedTotalRent%2CcontactDetails%2Cgrouping%2ClistingsInListFirstSummary%2Cnextgen%2CprojectsInAllRealestateTypes%2Cviareporting%2CvirtualTour&sorting=standard&pagesize=25&pricetype=calculatedtotalrent&geocodes={geoPath}&pagenumber={page}", headerType: "ImmoScout24", isPostRequest: false, body: "");

                        //parse result
                        var json = JsonConvert.DeserializeObject<dynamic>(responseString);
                        if (json["pageSize"].ToString() == "0") break;
                        var searchResults = json["results"];
                        foreach (var item in searchResults)
                        {
                            var tempObj = new ResultObj() { InputLocation = locationList[i] };

                            try { tempObj.ListingPrice = HttpUtility.HtmlDecode(item["attributes"][0]["value"].ToString()).Trim(); } catch (Exception) { }
                            try { tempObj.ListingAge = HttpUtility.HtmlDecode(item["published"].ToString()).Trim(); } catch (Exception) { }
                            try { tempObj.ListingTitle = HttpUtility.HtmlDecode(item["title"].ToString()).Trim(); } catch (Exception) { }
                            try { tempObj.ListingId = HttpUtility.HtmlDecode(item["id"].ToString()).Trim(); } catch (Exception) { }

                            allResultObjs.Add(tempObj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"({DateTime.Now.ToString("mmHH:mm")}): Error with context: {ex.ToString()}");
                }
            }
            #endregion

            #region Save scrape results
            allResultObjs.RemoveAt(0);
            File.WriteAllText($@"{_environmentPath}\Misc\Output\{outputFileName}", JsonConvert.SerializeObject(allResultObjs, Formatting.Indented));
            Console.WriteLine($"({DateTime.Now.ToString("HH:mm")}): Done. Results saved.");
            #endregion
        }
        private static string GetResponse(string linkToVisit, string headerType = "", bool isPostRequest = false, string body = "")
        {
            var client = new RestClient(linkToVisit)
            {
                UserAgent = "ImmoScout24_1195_28_._"
            };
            var request = GetAppropriateHeader(headerType, isPostRequest, body);

            var response = client.ExecuteAsync(request);

            if (!response.Wait(120000)) { return ""; };
            if (response.Result.Content.Length == 0) { }

            return response.Result.Content;
        }
        private static RestRequest GetAppropriateHeader(string headerType, bool isPostRequest, string body)
        {
            var request = new RestRequest();
            IEnumerable<FileInfo> headerFile = null;

            if (isPostRequest) request = new RestRequest(Method.POST);

            headerFile = new DirectoryInfo($@"{_environmentPath}\Misc\ApiCookies").GetFiles().Where(x => x.Name == $"{headerType}.txt");
            var fileContent = File.ReadAllLines(headerFile.First().FullName);
            foreach (var line in fileContent)
            {
                try
                {
                    var headerName = Regex.Match(line, @".+?(?=\:)").Value.Trim();
                    var headerValue = Regex.Match(line, @"(?<=:).*").Value.Trim();

                    //if (headerName.ToLower().Contains("accept-encoding")) { headerValue = "application/json"; }

                    if (headerName.ToLower().Contains("content-length")) { }
                    //else if (headerName.ToLower().Contains("host")) { }
                    else if (headerName.Contains("{") || headerValue.Contains("{")) { }
                    else request.AddHeader(headerName, headerValue);
                }
                catch (Exception) { }
            }
            if (body.Length > 0)
            {
                request.AddParameter("application/json", body, ParameterType.RequestBody);
            }

            return request;
        }
    }
    class ResultObj
    {
        public string ListingPrice { get; set; }
        public string ListingAge { get; set; }
        public string ListingTitle { get; set; }
        public string ListingId { get; set; }
        public string InputLocation { get; set; }
    }
}