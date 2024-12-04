using BookCatalogManagementSystem.Client.Models.Constants;

namespace BookCatalogManagementSystem.Client.SignalR;

using Microsoft.AspNetCore.SignalR.Client;

public class SignalRService
{
    private HubConnection _hubConnection;

    public SignalRService(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;
        StartConnection($"{ApiUrlAddress.ApiBaseAddress}/hubs/books");
    }

    // Event to notify subscribers when a new book is received
    public event Action<string>? OnBookReceived;

    // Start the SignalR connection
    public async Task StartConnection(string hubUrl)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .WithAutomaticReconnect()
            .Build();

        // Register the handler for the "ReceiveBook" SignalR event
        _hubConnection.On<string>("ReceiveBook", json =>
        {
            OnBookReceived?.Invoke(json);
        });

        await _hubConnection.StartAsync();
        Console.WriteLine("SignalR connection started.");
    }

    // Stop the SignalR connection
    public async Task StopConnection()
    {
        if (_hubConnection != null)
        {
            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();
            Console.WriteLine("SignalR connection stopped.");
        }
    }

    public ValueTask DisposeAsync()
    {
        return _hubConnection?.DisposeAsync() ?? ValueTask.CompletedTask;
    }
}