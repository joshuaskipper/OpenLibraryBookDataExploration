using System;
using System.ComponentModel;
using System.Text.Json;

namespace OpenLibraryBookDataExploration
{
    class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine(DateTime.Today.ToString("D"));


            string url = "https://openlibrary.org/subjects/science_fiction.json?limit=200";

            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var openlibrary = JsonSerializer.Deserialize<Root>(json);

                    foreach (var item in openlibrary.works)
                    {
                        Console.WriteLine(item.title);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
        }
    }
}