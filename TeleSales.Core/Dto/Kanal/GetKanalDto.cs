using TeleSales.DataProvider.Enums;

namespace TeleSales.Core.Dto.Kanal;

public class GetKanalDto
{
    public long id { get; set; }
    public string Name { get; set; }
    public bool isDeleted { get; set; }
    public string Type { get; set; }
    public DateTime CreateAt { get; set; }
}
