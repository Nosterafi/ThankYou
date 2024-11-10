using System;
using System.Collections.Generic;

namespace OnlineTipsService.Domain;

public partial class Bank
{
    public string BankCode { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public virtual ICollection<BankCard> BankCards { get; set; } = new List<BankCard>();
}
