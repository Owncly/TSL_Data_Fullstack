using Microsoft.AspNetCore.SignalR;


public class MatchEventSim : BackgroundService
{
    private readonly IHubContext<MatchHub> _hubContext;
    private readonly Random _random = new();

    public MatchEventSim(IHubContext<MatchHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var allPlayers = new[] { "Owain", "Dave", "Steve", "Bee", "James", "Bob", "Jim", "Potato" };
        //Making Random order
        var assigned = allPlayers.OrderBy(_ => _random.Next()).ToList();
        

        var actions = new[] { "serve", "smash", "drop", "drive" };

        while (!stoppingToken.IsCancellationRequested)
        {
            //Making random values for this
            var matchEvent = new MatchModel
            {
                Player = assigned[_random.Next(assigned.Count)],
                Action = actions[_random.Next(actions.Length)],
                GameId = _random.Next(1, 5), //1-4 courts
                Points = _random.Next(1, 21),
                Timestamp = DateTime.UtcNow
            };

            await _hubContext.Clients.All.SendAsync("ReceiveEvent", matchEvent);
            Console.WriteLine($"Court: {matchEvent.GameId} Player: {matchEvent.Player} Score: {matchEvent.Points} points Action: {matchEvent.Action}");
            await Task.Delay(1000, stoppingToken); //Send every second
        }
    }
}