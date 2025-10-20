using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

public class MatchHub : Hub
{
public async Task SendEvent(MatchModel e)
{
    //Verifying data is correct with ruleset made in MatchModel
    try
    {
        
        var context = new ValidationContext(e);
        Validator.ValidateObject(e, context, validateAllProperties: true);

        await Clients.All.SendAsync("ReceiveEvent", e);
    }
    catch (ValidationException ex)
    {
        Console.WriteLine($"Validation failed: {ex.Message}");

        await Clients.Caller.SendAsync("ReceiveError", $"Invalid data: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
        await Clients.Caller.SendAsync("ReceiveError", "An unexpected error occurred");
    }
}
}