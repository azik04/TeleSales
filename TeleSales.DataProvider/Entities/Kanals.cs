using TeleSales.DataProvider.Entities.BaseModel;

namespace TeleSales.DataProvider.Entities;

public class Kanals : Base
{
    public string Name { get; set; } 

    public virtual ICollection<Calls> Calls { get; set; }
}
