using System;
using System.Collections.Generic;

namespace LinQwithJoin.Models;

public partial class SchoolStudent
{
    public int? StudentId { get; set; }

    public string? SchoolName { get; set; }

    public int? StudentMark { get; set; }

    public int RollNumber { get; set; }

    public virtual Person? Student { get; set; }
}
