const int PERIOD = 1000;
const string WIKIPEDIA_RANDOM = "https://en.wikipedia.org/api/rest_v1/page/random/summary";
const string URLSHORTNER_ENDPOINT = "http://localhost:5000/api/urls";

var httpClient = new HttpClient();

var work = new System.Timers.Timer(PERIOD);
work.Elapsed += OnTimedEvent;
work.Start();

void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
{
    try
    {
        var url = httpClient.GetAsync(WIKIPEDIA_RANDOM).Result.Content.Headers.ContentLocation.ToString();
        var urlShort = httpClient.PostAsync(URLSHORTNER_ENDPOINT + "/" + url, null).Result.Content;
        Console.WriteLine($"{urlShort}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message+ex.StackTrace);
        Console.WriteLine(ex.InnerException?.Message+ex.InnerException?.StackTrace);
    }
}

Console.ReadLine();