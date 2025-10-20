using Microsoft.AspNetCore.SignalR;


public class MatchEventSim : BackgroundService
{
    private readonly IHubContext<MatchHub> _hubContext;
    private readonly Random _random = new();
    private readonly Dictionary<int, (string Player1, string Player2)> _courtAssignments = new();
    private readonly Dictionary<string, int> _playerScores = new();

    public MatchEventSim(IHubContext<MatchHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var allPlayers = new[] { "Owain", "Dave", "Steve", "Bee", "James", "Bob", "Jim", "Potato" };
        //Making Random order
        var assigned = allPlayers.OrderBy(_ => _random.Next()).ToList();
        
        //Making 4 courts with pairs for each so a player cannot be generated with the wrong court ID 
        for (int court = 1; court <= 4; court++) 
        {
            var p1 = assigned[(court - 1) * 2];
            var p2 = assigned[(court - 1) * 2 + 1];
            _courtAssignments[court] = (p1, p2);

            _playerScores[p1] = 0;
            _playerScores[p2] = 0;

            Console.WriteLine($"Court {court}: {p1} vs {p2}");
        }

        var actions = new[] { "serve", "smash", "drop", "drive" };

        while (!stoppingToken.IsCancellationRequested)
        {
            //Courts 1–4 range
            int courtId = _random.Next(1, 5); 
            var (p1, p2) = _courtAssignments[courtId];
            var player = _random.Next(2) == 0 ? p1 : p2;

            var currentScore = _playerScores[player];
            //1/4 the time the update will be an increase in score
            var shouldScore = _random.Next(4) == 0; 
            //Add one if 1/4th is hit, score should only increase
            var newScore = shouldScore ? currentScore + 1 : currentScore; 
            _playerScores[player] = newScore;

            var matchEvent = new MatchModel
            {
                Player = player,
                Action = actions[_random.Next(actions.Length)],
                GameId = courtId,
                Points = newScore,
                Timestamp = DateTime.UtcNow
            };

            await _hubContext.Clients.All.SendAsync("ReceiveEvent", matchEvent);
            Console.WriteLine($"Court: {courtId} Player: {player} Score: {newScore} Action: {matchEvent.Action}");
            await Task.Delay(1000, stoppingToken); //Send every second
        }
    }
}