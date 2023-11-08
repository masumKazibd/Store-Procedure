using System;
using System.Collections.Generic;

namespace StoreProcedure.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;
}
