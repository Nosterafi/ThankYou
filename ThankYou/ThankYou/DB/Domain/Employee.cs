using System;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;

namespace ThankYou.DB.Domain;

public partial class Employee
{
    public short Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public short MerchantId { get; set; }

    public float EmployeeRating { get; set; }

    public string Password { get; set; } = null!;

    public string Position { get; set; } = null!;

    public virtual Merchant Merchant { get; set; } = null!;

    public virtual Position PositionNavigation { get; set; } = null!;

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();

    public Image GetQRcode()
    {
        var adress = "http://localhost:7116/Main/Index/";
        var img = new QRCodeEncoder().Encode(adress + this.Id);

        return img;
    }
}