using TeleSales.DataProvider.Enums;
using TeleSales.DataProvider.Enums.Debitor;

namespace TeleSales.Core.Dto.Call;

public class ExcludeCallDto
{
    public CallResult Conclusion { get; set; }
    public string? Note { get; set; }
    public long? ExcludedBy { get; set; }
    public DateTime? LastStatusUpdate { get; set; }
    public DateTime? NextCall { get; set; }
}
