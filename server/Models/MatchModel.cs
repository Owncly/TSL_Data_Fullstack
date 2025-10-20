using System.ComponentModel.DataAnnotations;

public class MatchModel
{
    [Required]
    public required string Player { get; set; }
    public string? Action { get; set; }
    [Range(1, 20)]
    [Required]
    public int GameId { get; set; }

    [Range(0, 30)]
    [Required]
    public int Points { get; set; }
    [Required]
    public DateTime Timestamp { get; set; }
}