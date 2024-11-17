using System;
using System.Collections.Generic;

namespace ThankYou.DB.Domain;

public partial class Merchant
{
    public short Inn { get; set; }

    public string Address { get; set; } = null!;

    public string? Menu { get; set; }

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
