using TeleSales.DataProvider.Enums;

namespace TeleSales.Core.Dto.Call;

public class CreateCallDto
{
    public string Status { get; set; } // Tapşırığın statusu (Yeni, Yenidən zəng)
    public long KanalId { get; set; } // Kanal
    public string EntrepreneurName { get; set; } // Sahibkarın adı
    public string LegalName { get; set; } // Hüquqi adı
    public string VOEN { get; set; } // VÖEN
    public DateOnly? PermissionStartDate { get; set; } // İcazənin başlanma tarixi
    public string PermissionNumber { get; set; } // İcazə nömrəsi
    public string Address { get; set; } // Ünvan
    public string Phone {  get; set; }
    public bool IsExcluded { get; set; }
}

