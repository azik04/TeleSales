namespace TeleSales.Core.Dto.Call;

public class ExcludeCallDto
{

    public string? Note { get; set; }
    public long? UserId { get; set; }
    public DateTime? LastStatusUpdate { get; set; } // Tracks when the status was last updated
    public string? Conclusion { get; set; }
    public DateTime? NextCall { get; set; }
}
