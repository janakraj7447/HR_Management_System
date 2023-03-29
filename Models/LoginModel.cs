using System;
using System.Collections.Generic;

namespace Candidate_Management_System.Models;

public partial class LoginModel
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public bool IsActive { get; set; }
}