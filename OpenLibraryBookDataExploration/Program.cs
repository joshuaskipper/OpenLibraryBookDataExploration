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


            string url = "https://pokeapi.co/api/v2/pokemon?limit=200&utm_source=chatgpt.com";

            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
        }
    }
}