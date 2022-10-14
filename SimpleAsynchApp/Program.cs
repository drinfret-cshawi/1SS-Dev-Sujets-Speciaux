namespace SimpleAsynchApp;

public class Program
{
    private static readonly List<string> Urls = new()
        { "https://google.com", "https://www.cegepshawinigan.ca", "https://github.com" };

    private static readonly HttpClient HttpClient = new();

    public static async Task Main(string[] args)
    {
        Console.WriteLine("------------------------ 1 ------------------------");
        GetData("https://google.com");
        Console.WriteLine("------------------------ 2 ------------------------");
        // sans await, la méthode async est appelée, mais on n'attend pas avant de continuer
        await GetDataAsync("https://google.com");
        Console.WriteLine("------------------------ 3 ------------------------");

        foreach (string url in Urls)
        {
            Console.WriteLine($"4 ------------------------ {url} ------------------------");
            GetData(url);
        }

        foreach (string url in Urls)
        {
            Console.WriteLine($"5 ------------------------ {url} ------------------------");
            await GetDataAsync(url);
        }

        Console.WriteLine("------------------------ 6 ------------------------");

        // télécharger toutes les pages en même temps, et attendre qu'elles aient toutes terminées avant de continuer
        Task.WaitAll(Urls.Select((url) => GetDataAsync(url)).ToArray());
        Console.WriteLine("------------------------ 7 ------------------------");
    }

    private static void GetData(string url)
    {
        // pas idéal, de cette façon, l'exécution devient synchrone
        // sans await, on peut accéder aux résultats avec Result, mais l'éxécution va être bloquée
        // et les exceptions ne seront pad gérées
        var stringData = HttpClient.GetStringAsync(url);
        Console.WriteLine(url + stringData.Result[..25]);
    }

    private static async Task GetDataAsync(string url)
    {
        // avec await, on attend que la méthode soit complétée et retourne une valeur, 
        // donc à première vue, ça ne change pas grand chose parce qu'on attend quand même
        // mais s'il y avait d'autres fils d'exécution (thread) dans l'app, ils auraient une chance d'être exécutés
        // comme dans une applications graphique, au lieu de bloquer toute l'app quand l'utilisateur clique
        // sur un bouton, l'interface peut se mettre à jour en attendant que la méthode se termine
        var stringData = await HttpClient.GetStringAsync(url);
        Console.WriteLine(url + stringData[..25]);
    }
}