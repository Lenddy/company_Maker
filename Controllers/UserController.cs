using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using company.Models;

namespace company.Controllers;
//todo  think about what you need to update

public class UserController : Controller
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
public UserController(DB context){
    db = context;
}

[HttpGet("/")]
public IActionResult index(){
    if(loggedIn){//? redirecting to the home page if user is logged in 
        return RedirectToAction("home","company");
    }
    return View("index");
}


[HttpPost("/register")]
public IActionResult register(User newUser){
        if(db.Users.Any(user=> user.Email == newUser.Email) ){//? checking to see if the email inputted already exist
            ModelState.AddModelError("email", "this email is already taken ");//? adding a validation error
        }
        
        if(ModelState.IsValid == false){//? you need to check a second time for validations 
            return index();
        }
        PasswordHasher<User> hash = new PasswordHasher<User>();//* this is fo hashing the passwords
        newUser.Password = hash.HashPassword(newUser,newUser.Password);
        // we need to add it to the list  and save the changes
        db.Users.Add(newUser);//? we need to add it to the list  and save the changes
        db.SaveChanges(); 
        HttpContext.Session.SetInt32("uid", newUser.UserId);// sending the new users id to session
        HttpContext.Session.SetString("uName",newUser.fullName());
        return Redirect("/congratulations");
}

    // for when a new user makes an account
    [HttpGet("/congratulations")]
    public IActionResult congratulations(){
        if(!loggedIn){
            return RedirectToAction("index");
        }
        return View("congratulations");
    }



[HttpPost("/logIn")]
public IActionResult logIn(Login logInUser){
    if(ModelState.IsValid == false){
        return index();
    }
    User? user = db.Users.FirstOrDefault(user=> user.Email == logInUser.LoginEmail);//? chekiang if there is a user in the data base if the info inputted pass the validations
    
    if(user == null){//? if there is no user matching the info we will show a validation error message
        ModelState.AddModelError("loginEmail","email/password does not match");
        return index();
    }
    PasswordHasher<Login> hash = new PasswordHasher<Login>();//? if the user exist
    PasswordVerificationResult compare = hash.VerifyHashedPassword(logInUser,user.Password,logInUser.LoginPassword);
    //* this is how you check for errors
    if (compare == 0){// something will be store as a 0 if you ge a n error 
        ModelState.AddModelError("loginEmail","email/password does not match");
        return index();
    }
    // if there is no error return then log the user in 
    HttpContext.Session.SetInt32("uid",user.UserId);
    HttpContext.Session.SetString("uName",user.fullName());
    return Redirect("/home");
}

//* this is going to be profile make it tomorrow
    [HttpGet("user/{id}")]
    public IActionResult profile(int id){
            if(!loggedIn){
            return RedirectToAction("index");
        }
        //* might want to redirect the user to the update page or some were else doit tomorrow because the user does not need to only to se there info they also will need to updated or add more things also start learning how to use socket.io for the  massaging  part of the web site

    return View("profile");
    }



    [HttpGet("/user/{id}/edit")]
    public IActionResult edit(int id){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        User? oneUser = db.Users.FirstOrDefault(c => c.UserId == id);
        if(oneUser == null){
            return RedirectToAction("home","Company");
        }
        return View("userInfo", oneUser);
    }

    [HttpPost("/user/{id}/update")]
    public IActionResult update(int id,User updatedUser){
        if(!loggedIn || uid == null){
            return RedirectToAction("index","User");
        }
        if(ModelState.IsValid == false){
            return edit(id);
        }
        User? toBeUpdated = db.Users.FirstOrDefault(c => c.UserId == id);
        if(toBeUpdated == null){
            return RedirectToAction("home");
        }
        toBeUpdated.F_name = updatedUser.F_name;
        toBeUpdated.L_name = updatedUser.L_name;
        // toBeUpdated.Email = updatedUser.Email;
        // toBeUpdated.creators = updatedUser.creators;
        toBeUpdated.phoneNumber = updatedUser.phoneNumber;
        toBeUpdated.address = updatedUser.address;
        toBeUpdated.UpdatedAt = DateTime.Now;

        db.Users.Update(toBeUpdated);
        db.SaveChanges();

        return profile(toBeUpdated.UserId);
    }

    [HttpPost("/logout")]
    public IActionResult logout(){
    HttpContext.Session.Clear();
    return RedirectToAction("index");
}
}