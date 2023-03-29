using System;
using System.Collections.Generic;

namespace Candidate_Management_System.Entities;

public partial class CandidateInformation
{
    public int Id { get; set; }

    public string? CandidateName { get; set; }

    public string? Image { get; set; }

    public string? Dob { get; set; }

    public string? Address { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? Technology { get; set; }

    public string? Resume { get; set; }

    public string? Description { get; set; }
}
