using PieShopConsoleClient;
using System.Text;
using System.Text.Json;

Console.WriteLine("Press any key to get all pies");
Console.ReadKey();

using var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7043");

var response = await client.GetAsync("pies");
response.EnsureSuccessStatusCode();

var pies = await response.Content.ReadAsStringAsync();

var piesList = JsonSerializer.Deserialize<List<Pie>>(pies, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

foreach (var pie in piesList)
{
    Console.WriteLine($"{pie.Name} - {pie.Description}");
}

Console.WriteLine();
Console.ReadKey();


Console.WriteLine("Press any key to add a new pie");
Console.ReadKey();


var newPie = new Pie { Name = "New Apple Pie", Description = "Improved apple pie" };
var newPieJson = JsonSerializer.Serialize(newPie);
var content = new StringContent(newPieJson, Encoding.UTF8, "application/json");

response = await client.PostAsync("pies", content);
response.EnsureSuccessStatusCode();

Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.ReadKey();