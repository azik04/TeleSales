namespace TeleSales.Core.Dto.UserKanal;

public class GetUserKanalDto
{
    public long KanalId { get; set; }
    public string? KanalName { get; set; }
    public long UserId { get; set; }
    public string? UserEmail { get; set; }
    public string Type {  get; set; }
}
