#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
//your namespace
namespace company.Models;    //must be the same that is on you program file 
//classname
public class Company
{
//* you need to use
//dotnet ef migrations add FirstMigration
//dotnet ef database update
//* only doit after creating you routes with all the info that you need
//this is the primary key
    [Key]
// always match the name of the class + id
    public int CompanyId { get; set; }
//change the field as needed

    [Required]
    [MinLength(3,ErrorMessage = "the name must be at least 3 character long ")]
    [Display(Name ="Company name")]
    public string companyName { get; set; }

    [Required]
    [MinLength(3,ErrorMessage = "the type must be at least 3 character long ")]
    [Display(Name ="type")]
    public string companyType { get; set; }

    [Required]
    [MinLength(5,ErrorMessage = "the description must be at least 5 character long ")]
    [Display(Name ="Company description")]
    public string companyDescription { get; set; }
    [Required]
    [MinLength(2,ErrorMessage = "the creators name must be at least 2 character long ")]
    [Display(Name ="Company creator")]
    public string creators {get; set;}


    [DataType(DataType.PhoneNumber)]
    [Display(Name = "company Phone Number(optional)")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
    public string ?companyPhoneNumber { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [Display(Name ="company email address")]
    public string email {get; set;}

    [Display(Name ="company photo(optional)")]
    public string ?image {get; set;}

    [Display(Name ="company creation date(optional)")]
    [DataType(DataType.Date)]
    public DateTime ?dateCreated {get; set;}

    [Required]
    [MinLength(1,ErrorMessage = "you must add a country/region name ")]
    [Display(Name ="country/region")]
    public string country {get; set;}
    
    [Required]
    [MinLength(1,ErrorMessage = "you must add city")]
    [Display(Name ="city")]
    public string city {get; set;}

    [Required]
    [MinLength(1,ErrorMessage = "you must add a company address")]
    [Display(Name ="address")]
    public string address {get; set;}

    [Required]
    [Display(Name ="postal Code")]
    [Range(0,Int32.MaxValue)]
    public int postalCode {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    public int UserId {get; set;}
    public User? companyCreator {get; set;}
}