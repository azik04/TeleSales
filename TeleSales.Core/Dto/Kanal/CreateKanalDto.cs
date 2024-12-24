using TeleSales.DataProvider.Enums;

namespace TeleSales.Core.Dto.Kanal;

public class CreateKanalDto
{
    public string Name { get; set; }
    public KanalType Type { get; set; }
}
