using TeleSales.DataProvider.Entities.BaseModel;
using TeleSales.DataProvider.Enums;

namespace TeleSales.DataProvider.Entities;

public class Kanals : Base
{
    public string Name { get; set; } 
    public KanalType Type { get; set; }
    public virtual ICollection<Calls> Calls { get; set; }
    public virtual ICollection<UserKanals> UserKanal {  get; set; }
    
}
