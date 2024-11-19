namespace TeleSales.DataProvider.Entities.BaseModel;

public class Base
{
    public long id { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreateAt { get; set; }
}
