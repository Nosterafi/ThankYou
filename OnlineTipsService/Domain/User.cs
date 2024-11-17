using System;
using System.Collections.Generic;

namespace ThankYouDB.Domain;

public partial class User
{
    public short Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;
}
