using System.ComponentModel;

namespace Uplift.Models
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
        [Description("Pending")]
        Pending,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected
    }
}