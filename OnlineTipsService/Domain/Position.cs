﻿using System;
using System.Collections.Generic;

namespace OnlineTipsService.Domain;

public partial class Position
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
