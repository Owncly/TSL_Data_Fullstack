using System.ComponentModel.DataAnnotations;

public class MatchModel
{
    [Required]
    public required string Player { get; set; }
    //Action is not required if just a score update is sent
    public string? Action { get; set; }
    
    [Range(1, 21)]
    [Required]
    public int GameId { get; set; }
    //Range will be 30 as per standard rules, no game points should be higher than
    [Range(0, 30)]
    [Required]
    public int Points { get; set; }
    [Required]
    public DateTime Timestamp { get; set; }
}