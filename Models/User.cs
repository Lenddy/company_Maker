#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//your namespace
namespace company.Models;    //must be the same that is on you program file 
//classnamepublic class Users

//* you need to use
//dotnet ef migrations add FirstMigration
//dotnet ef database update
//* only doit after creating you routes with all the info that you need
public class User{
//this is the primary key
    [Key]
    public int UserId { get; set; }
//change the field as needed
    [Required]
    [MinLength(2,ErrorMessage ="must be at least 2 character long ")]
    [Display( Name ="First name")]
    public string F_name { get; set; }

    [Required]
    [MinLength(2,ErrorMessage ="must be at least 2 character long ")]
    [Display(Name = "Last name")]
    public string L_name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(8,ErrorMessage ="must be at least 8 character long ")]
    [DataType(DataType.Password)]
    public string Password {get; set;}
    
    [NotMapped]
    [Compare("Password",ErrorMessage = "passwords does not match")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string Confirm_password {get; set; }

    [MinLength(5,ErrorMessage ="address must be at least 5 characters long ")]
    [Display(Name ="Address(optional)")]
    public string ?address {get; set;}

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "company Phone Number(optional)")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
    public string ?phoneNumber {get; set;}

    //* this is a place holder for the age
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
// for one to many
// * this is for when you make the job side of the website
    // public List<Company> createdCompanies {get; set;} = new List<Company>();

    // public List<Association> attendingWedding {set; get;} = new List<Association>();

    public string fullName(){
        return $"{F_name} {L_name}";
    }
}
