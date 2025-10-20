using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

public class MatchHub : Hub
{
public async Task SendEvent(MatchModel e)
{
    var context = new ValidationContext(e);
    Validator.ValidateObject(e, context, validateAllProperties: true);

    await Clients.All.SendAsync("ReceiveEvent", e);

}
}