 using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Candidate_Management_System.Models;
public partial class Candidate
{
    public int Id { get; set; }
    [Required]
   
    public string? CandidateName { get; set; }

[Required(ErrorMessage ="Image should be in JPEG format")]
    public string? Image { get; set; }

     [Required]
    public string? Dob { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Age must be between 1-100 in years.")]
    public string? Mobile { get; set; }
   [Required]
   [EmailAddress (ErrorMessage ="invalid email")]
    public string? Email { get; set; }
[Required]
    public string? Technology { get; set; }
[Required(ErrorMessage ="Resume should be in Pdf or word document format only")]
    public string? Resume { get; set; }
[Required]
    public string? Description { get; set; }
}

public enum tech
{
    dotnet,
    php,
    QA,
    Java  
}
