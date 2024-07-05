using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Country
{
    public string CountryId { get; set; } = null!;

    public string? CountryName { get; set; }

    public int? RegionId { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual Region? Region { get; set; }
}
