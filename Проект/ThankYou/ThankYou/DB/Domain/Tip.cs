﻿namespace ThankYou.DB.Domain;

public partial class Tip
{
    public int Id { get; set; }

    public short? ClientId { get; set; }

    public short EmployeeId { get; set; }

    public short Sum { get; set; }

    public short Grade { get; set; }

    public string? Review { get; set; }

    public DateOnly Date { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
