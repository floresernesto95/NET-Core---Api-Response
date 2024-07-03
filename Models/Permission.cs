using System;
using System.Collections.Generic;

namespace Project_Courses.Models;

public partial class Permission
{
    public Guid Id { get; set; }

    public string Module { get; set; } = null!;

    public string Feature { get; set; } = null!;

    public string? Description { get; set; }

    public bool Enabled { get; set; }
}
