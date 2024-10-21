using POSClientApplet.Services;

namespace POSClientApplet.BackgroundServices;

public class ValidationHostedService : IHostedService
{
    private readonly IUserValidationService _userValidationService;
    private readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(1);

    public ValidationHostedService(IUserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await _userValidationService.ValidatePendingUsers();
            await Task.Delay(_pollingInterval, cancellationToken); 
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
