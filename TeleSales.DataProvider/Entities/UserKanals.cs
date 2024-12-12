using TeleSales.DataProvider.Entities.BaseModel;

namespace TeleSales.DataProvider.Entities;

public class UserKanals : Base
{
    public long UserId { get; set; }
    public long KanalId { get; set; }

    public virtual Users Users { get; set; }
    public virtual Kanals Kanals { get; set; }
}
