using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using company.Models;
using Microsoft.EntityFrameworkCore;

namespace company.Controllers;
//todo  think about what you need to update

public class CompanyController : Controller
{
private DB db;
// creating private field redirect people to hte index page if tey are not log in 

private int? uid{
    get{
        return HttpContext.Session.GetInt32("uid");
    }
}
private bool loggedIn{
get{
    return uid!= null;
}
}
public CompanyController(DB context){
    db = context;
}
    [HttpGet("/home")]
    public IActionResult home(){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        List<Company> allCompanies = db.Companies.Include(u => u.companyCreator).ToList();
        return View("home",allCompanies);
    }

    [HttpGet("/company/new")]
    public IActionResult newCompany(){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        return View("newCompany");
    }

    [HttpPost("/company/create")]
    public IActionResult create(Company createCompany){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        if(ModelState.IsValid == false){ // this needs to be change is any error happens
            return newCompany();
        }
        createCompany.UserId = (int)uid;// this is going to ge the id of the user that creates the company
        db.Companies.Add(createCompany);
        db.SaveChanges();
        return RedirectToAction("home","Company");
    }

    [HttpGet("/company/{id}")]

    public IActionResult viewOne(int id){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        Company? oneCompany = db.Companies.Include(u=> u.companyCreator).FirstOrDefault(c => c.CompanyId == id);
        if(oneCompany == null){
            return RedirectToAction("home");
        }
        return  View("showOne",oneCompany);
    }
    
    [HttpGet("/company/{id}/edit")]
    public IActionResult edit(int id){
                if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        Company? oneCompany = db.Companies.Include(u=> u.companyCreator).FirstOrDefault(c => c.CompanyId == id);
        if(oneCompany == null){
            return RedirectToAction("home");
        }
        return View("edit", oneCompany);
    }

    [HttpPost("/company/{id}/update")]

    public IActionResult update(int id,Company updatedCompany){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        if(ModelState.IsValid == false){
            return edit(id);
        }
        Company? toBeUpdated = db.Companies.FirstOrDefault(c => c.CompanyId == id);
        if(toBeUpdated == null){
            return RedirectToAction("home");
        }
        toBeUpdated.companyName = updatedCompany.companyName;
        toBeUpdated.companyType = updatedCompany.companyType;
        toBeUpdated.companyDescription = updatedCompany.companyDescription;
        toBeUpdated.creators = updatedCompany.creators;
        toBeUpdated.companyPhoneNumber = updatedCompany.companyPhoneNumber;
        toBeUpdated.email = updatedCompany.email;
        toBeUpdated.image = updatedCompany.image;
        toBeUpdated.dateCreated = updatedCompany.dateCreated;
        toBeUpdated.city = updatedCompany.city;
        toBeUpdated.country = updatedCompany.country;
        toBeUpdated.address = updatedCompany.address;
        toBeUpdated.postalCode = updatedCompany.postalCode;
        toBeUpdated.UpdatedAt = DateTime.Now;

        db.Companies.Update(toBeUpdated);
        db.SaveChanges();

        return viewOne(toBeUpdated.CompanyId);
    }

    [HttpPost("/company/{id}/delete")]
    public IActionResult delete(int id){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        if(ModelState.IsValid == false){
            return RedirectToAction("home");
        }
        Company? oneCompany = db.Companies.FirstOrDefault(d => d.CompanyId == id);
        if(oneCompany == null || oneCompany.UserId != (int)uid){
            return home();
        }
        db.Remove(oneCompany);
        db.SaveChanges();
        return RedirectToAction("home");
    }

//! when you make the jobs model and controller chose if you what to be necessary to create a company  before they are able to create a job  if you chose that is necessary to have a company make it that only when the company is created that a prom or button  appears  ans also make a drop down menu that holds all that companies that the user ha created  also make a counter go up or that show the amount of jobs created as available jobs  in the job side make a counter that shows the amount of people that have apply to the job the job description and contact info and location chose if that location of the job is going to be the same as the location of the company  or not  if you chose that a company has to be created before hand  also fix that profile and update user pages and figure out how to allow the users how to change their name and the password
}