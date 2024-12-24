using TeleSales.DataProvider.Entities.BaseModel;
using TeleSales.DataProvider.Enums;

namespace TeleSales.DataProvider.Entities;

public class Users : Base
{
    public string FullName { get; set; } 
    public string Email { get; set; } 
    public string Password { get; set; } 
    public Role Role { get; set; }

    public virtual ICollection<Calls> Calls { get; set; }
    public virtual ICollection<UserKanals> UserKanal { get; set; }
    public virtual ICollection<CallCenters> CallCenter { get; set; }

}
