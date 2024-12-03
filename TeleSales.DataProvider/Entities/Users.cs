using TeleSales.DataProvider.Entities.BaseModel;

namespace TeleSales.DataProvider.Entities;

public class Users : Base
{
    public string FullName { get; set; } 
    public string Email { get; set; } 
    public string Password { get; set; } 

    public virtual ICollection<Calls> Calls { get; set; } = new List<Calls>();
}
