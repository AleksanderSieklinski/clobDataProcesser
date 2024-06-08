using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
DocumentLibrary library = new DocumentLibrary();
bool continueRunning = true;
do
{
    Console.WriteLine("Welcome to the Document Library!");
    Console.WriteLine("Please select an operation:");
    Console.WriteLine("1. Post a document");
    Console.WriteLine("2. Get all documents");
    Console.WriteLine("3. Update a document");
    Console.WriteLine("4. Delete a document");
    Console.WriteLine("5. Search documents");
    Console.WriteLine("6. Exit");

    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.WriteLine("Enter the content of the document:");
            string? content = Console.ReadLine();
            if (content != null)
                await library.PostDocument(content);
            break;
        case "2":
            await library.GetAll();
            break;
        case "3":
            Console.WriteLine("Enter the ID of the document to update:");
            string? idToUpdate = Console.ReadLine();
            Console.WriteLine("Enter the new content of the document:");
            string? newContent = Console.ReadLine();
            if (idToUpdate != null && newContent != null)
                await library.UpdateDocument(newContent, idToUpdate);
            break;
        case "4":
            Console.WriteLine("Enter the ID of the document to delete:");
            string? idToDelete = Console.ReadLine();
            if (idToDelete != null)
                await library.DeleteDocument(idToDelete);
            break;
        case "5":
            Console.WriteLine("Enter the search query:");
            string? query = Console.ReadLine();
            if (query != null)
                await library.Search(query);
            break;
        case "6":
            continueRunning = false;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
} while (continueRunning);
public class DocumentLibrary
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string baseUrl = "http://127.0.0.1:5000";
    public async Task<HttpResponseMessage> GetAll()
    {
        Console.WriteLine("Running GetAll operation...");
        HttpResponseMessage response = await client.GetAsync($"{baseUrl}/getAll");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response;
    }
    public async Task<HttpResponseMessage> PostDocument(string content)
    {
        Console.WriteLine("Running PostDocument operation...");
        var newDocument = new { content = content };
        var newDocumentJson = new StringContent(JsonConvert.SerializeObject(newDocument), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{baseUrl}/documents", newDocumentJson);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response;
    }
    public async Task<HttpResponseMessage> UpdateDocument(string content, string id)
    {
        Console.WriteLine("Running UpdateDocument operation...");
        var updatedDocument = new { content = content };
        var updatedDocumentJson = new StringContent(JsonConvert.SerializeObject(updatedDocument), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PutAsync($"{baseUrl}/documents/{id}", updatedDocumentJson);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response;
    }
    public async Task<HttpResponseMessage> DeleteDocument(string id)
    {
        Console.WriteLine("Running DeleteDocument operation...");
        HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}/documents/{id}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response;
    }
    public async Task<HttpResponseMessage> Search(string query)
    {
        Console.WriteLine("Running Search operation...");
        HttpResponseMessage response = await client.GetAsync($"{baseUrl}/search?query={query}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response;
    }
}