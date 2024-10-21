using System.Net.Http.Json;

namespace Client;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public short Status { get; set; }
}

class Program
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static readonly string _mevServiceBaseUrl = "http://localhost:5000/";
    private static readonly string _mealsyHrBaseUrl = "http://localhost:5001/";
    private static readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(1);

    static async Task Main(string[] args)
    {
        string? name = null;
        while (name != "exit")
        {
            Console.Write("Username: ");
            name = Console.ReadLine();

            // Step 1: Create a new user
            var newUser = new User { Name = name };
            var createUserResponse = await _httpClient.PostAsJsonAsync($"{_mealsyHrBaseUrl}api/users", newUser);
            createUserResponse.EnsureSuccessStatusCode();

            var createdUserResponse = await createUserResponse.Content.ReadFromJsonAsync<User>();
            int userId = createdUserResponse.Id;
            Console.WriteLine($"User created with Id: {userId}");

            // Step 2: Poll for validation status
            bool isValidated = false;
            while (!isValidated)
            {
                var getUserResponse = await _httpClient.GetAsync($"{_mevServiceBaseUrl}api/users/{userId}");
                if (getUserResponse.IsSuccessStatusCode)
                {
                    var user = await getUserResponse.Content.ReadFromJsonAsync<User>();
                    Console.WriteLine($"User Status: {user.Status}");

                    if (user.Status != 3) // 3 is Pending, so we check if status has changed
                    {
                        isValidated = true;
                        Console.WriteLine("User validation is complete.");
                    }
                    else
                    {
                        Console.WriteLine("User validation is still pending. Retrying in 1 second...");
                        await Task.Delay(_pollingInterval);
                    }
                }
            }
        }
    }
}
