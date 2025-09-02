using System;
using System.ComponentModel;
using System.Text.Json;

namespace OpenLibraryBookDataExploration
{
    class Program
    {
//test pull
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

                    // Group by author - but we need to handle multiple authors per book
                    var booksByAuthor = from book in openlibrary.works
                                        //where book.first_publish_year < 1950
                                        from author in book.authors 
                                        group book by author.name into authorGroup
                                        orderby authorGroup.Key 
                                        select authorGroup;

                    Console.WriteLine("Science Fiction Books from openlibrary.org api, Grouped by Author:\n");

                    foreach (var authorGroup in booksByAuthor)
                    {
                        Console.WriteLine($"\nAuthor: {authorGroup.Key}");

                        foreach (var book in authorGroup)
                        {
                            Console.WriteLine($"  - {book.title} ({book.first_publish_year})");
                        }
                        
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
