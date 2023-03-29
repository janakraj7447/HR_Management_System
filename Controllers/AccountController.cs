using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Candidate_Management_System.Models;
using Candidate_Management_System.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Candidate_Management_System.Controllers;

// [Route("[controller]/[action]")]
public class AccountController : Controller
{

   
    private readonly ILogger<AccountController> _logger;
    public AccountController(ILogger<AccountController> logger){
       _logger=logger;
    }

   

    public IActionResult Index()
    {
        return View();
    }

   

 
    

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]

    public IActionResult Login(LoginModel loginModel){
        using(var context=new CandidateDBContext()){
            var userDetails=context.Logins.Where(x=>x.UserName==loginModel.UserName && x.Password==loginModel.Password && x.IsActive==true).FirstOrDefault();
            if(userDetails!=null){
                var claims=new Claim[]{new Claim(ClaimTypes.Name,userDetails.UserName),new Claim(ClaimTypes.Role,userDetails.Role)};

                var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity));
                return RedirectToAction("candidate_info","Home");
            }
            else{
                ViewData["Errormsg"]="Incorrect username or Password";
                return View("Index");
            }
        }
        return View();
    }


    public IActionResult Logout(){
        HttpContext.SignOutAsync();
        return RedirectToAction("Index","Account");
  
    }
    



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
