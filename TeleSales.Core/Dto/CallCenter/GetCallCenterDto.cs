using TeleSales.DataProvider.Entities;

namespace TeleSales.Core.Dto.CallCenter;

public class GetCallCenterDto
{
    public long id { get; set; }
    public DateTime CreateAt { get; set; }
    public bool IsDeleted { get; set; }
    public string FullName { get; set; }
    public string Region { get; set; }
    public string Phone { get; set; }
    public long ExcludedBy { get; set; }
    public string ExcludedByName { get; set; }
    public long VOEN { get; set; }
    public string ApplicationType { get; set; }
    public string ShortContent { get; set; }
    public string DetailsContent { get; set; }
    public bool Forwarding { get; set; }
    public string? Administration { get; set; }
    public string? Department { get; set; }
    public long? ForwardTo { get; set; }
    public string Conclusion { get; set; }
    public string? Addition { get; set; }
    public long kanalId { get; set; }

}
