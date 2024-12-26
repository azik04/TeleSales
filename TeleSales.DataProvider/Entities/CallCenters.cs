using TeleSales.DataProvider.Entities.BaseModel;
using TeleSales.DataProvider.Entities;
using TeleSales.DataProvider.Enums.CallCenter;

public class CallCenters : Base
{
    public long kanalId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public long ExcludedBy { get; set; } 
    public long VOEN { get; set; }
    public string ShortContent { get; set; }
    public string DetailsContent { get; set; }
    public Region Region { get; set; }
    public ApplicationTypes ApplicationType { get; set; }
    public string Conclusion { get; set; }
    public bool isForwarding { get; set; }
    public Department? Department { get; set; }
    public string? ForwardTo { get; set; }
    public string? Addition { get; set; }

    public Kanals Kanal { get; set; }
    public Users Users { get; set; } 
}
