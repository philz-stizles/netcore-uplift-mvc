using System.ComponentModel;

namespace Uplift.Utility
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Manager")]
        Manager
    }

    public enum OrderStatus
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Manager")]
        Manager
    }
}