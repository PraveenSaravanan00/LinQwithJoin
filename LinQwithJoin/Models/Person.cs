using System;
using System.Collections.Generic;

namespace LinQwithJoin.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string PersonName { get; set; } = null!;

    public string PersonGender { get; set; } = null!;

    public int PersonAge { get; set; }

    public string PersonCity { get; set; } = null!;

    public virtual ICollection<SchoolStudent> SchoolStudents { get; set; } = new List<SchoolStudent>();
}
