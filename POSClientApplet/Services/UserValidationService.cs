using POSClientApplet.Models;

namespace POSClientApplet.Services;

public class UserValidationService : IUserValidationService
{
    private readonly HttpClient _httpClient;

    public UserValidationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ValidatePendingUsers()
    {
        var pendingUsers = await _httpClient.GetFromJsonAsync<User[]>("api/users/pending");
        foreach (var user in pendingUsers)
        {
            var isValid = ValidateExternally(user);

            // Update user status based on validation
            short newStatus = isValid ? (short)1 : (short)-1; // Active or Failed
            await _httpClient.PostAsJsonAsync($"api/users/update/{user.Id}", newStatus);
        }
    }

    private bool ValidateExternally(User user)
    {
        // Web-Srm validation logic simulation
        return user.Name.Length % 2 == 0;
    }
}
