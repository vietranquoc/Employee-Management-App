using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class AccountMember
{
    public string MemberId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }
}
