namespace ThankYou.DB.Domain;

public partial class BankCard
{
    public string CardNumber { get; set; } = null!;

    public short Owner { get; set; }

    public string BankNumber { get; set; } = null!;

    public virtual Bank BankNumberNavigation { get; set; } = null!;
}
