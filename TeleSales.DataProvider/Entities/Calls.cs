using TeleSales.DataProvider.Entities.BaseModel;

namespace TeleSales.DataProvider.Entities;

public class Calls : Base
{
    public string Status { get; set; } // Tapşırığın statusu (Yeni, Yenidən zəng)
    public long KanalId { get; set; } // Kanal
    public string EntrepreneurName { get; set; } // Sahibkarın adı
    public string LegalName { get; set; } // Hüquqi adı
    public string VOEN { get; set; } // VÖEN
    public DateOnly? PermissionStartDate { get; set; } // İcazənin başlanma tarixi
    public string PermissionNumber { get; set; } // İcazə nömrəsi
    public string Address { get; set; } // Ünvan
    public string Phone { get; set; }    // Əlaqə məlumatları
    public bool isDone { get; set; }

    public string? Conclusion { get; set; }
    public string? Note { get; set; }
    public long? UserId { get; set; }
    public DateTime? LastStatusUpdate { get; set; } // Tracks when the status was last updated
    public DateTime? NextCall { get; set; }


    public virtual Users User { get; set; }
    public virtual Kanals Kanal { get; set; }

}
