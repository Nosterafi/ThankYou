using System;
using System.Collections.Generic;

namespace OnlineTipsService.Domain;

public partial class Merchant
{
    public short Inn { get; set; }

    public string Address { get; set; } = null!;

    public string? Menu { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
