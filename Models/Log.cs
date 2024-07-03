using System;
using System.Collections.Generic;

namespace Project_Courses.Models;

public partial class Log
{
    public Guid Id { get; set; }

    public string Data { get; set; } = null!;

    public Guid DataId { get; set; }

    public DateTime Date { get; set; }

    public string User { get; set; } = null!;
}
