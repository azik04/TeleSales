using TeleSales.DataProvider.Entities;
using TeleSales.DataProvider.Enums.CallCenter;

namespace TeleSales.Core.Dto.CallCenter;

public class CreateCallCenterDto
{
    public long kanalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Region Region { get; set; }
    public string Phone { get; set; }
    public long ExcludedBy { get; set; }
    public long VOEN { get; set; }
    public ApplicationTypes ApplicationType { get; set; }
    public string ShortContent { get; set; }
    public string DetailsContent { get; set; }
    public bool Forwarding { get; set; }
    public Department? Department { get; set; }
    public string? ForwardTo { get; set; }
    public string Conclusion { get; set; }
    public string? Addition { get; set; }
}
