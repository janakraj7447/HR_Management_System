using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Candidate_Management_System.Models;
using Candidate_Management_System.Entities;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
 
namespace Candidate_Management_System.Controllers;

[Authorize]
public class HomeController : Controller
{

    private CandidateDBContext db=new CandidateDBContext();
    private readonly ILogger<HomeController> _logger;
 private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public HomeController(ILogger<HomeController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
    {   
        _logger = logger;
        Environment = _environment;
        
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult candidate_info(int Id, Candidate candidate)
    {
      
        
        return View(candidate);
    }

    public IActionResult candidate_info_view(int Id)
    {
        using (var context = new CandidateDBContext())
        {
            var candidateList = context.CandidateInformations.FirstOrDefault(x=>x.Id==Id);
            // var candidateList = context.CandidateInformations.Find(Id);
            return View(candidateList);
        }

    }
    [HttpGet]

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult candidate_view(string Sorting_Order, string Search_Data)
    {
         ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";  
        ViewBag.SortingDate = Sorting_Order == "Date_Enroll" ? "Date_Description" : "Date";  
    var students = from stu in db.CandidateInformations select stu;
    //if search box does not empty then this will run
    if(!string.IsNullOrEmpty(Search_Data)){
        students = students.Where(stu => stu.CandidateName.Contains(Search_Data));  
    }
   
    switch (Sorting_Order)  
    {  
        case "Name_Description":  
        students = students.OrderByDescending(stu=> stu.CandidateName);  
        break;  
        case "Date_Enroll":  
        students = students.OrderBy(stu => stu.Dob);  
        break;  
        case "Date_Description":  
        students = students.OrderByDescending(stu => stu.Dob);  
        break;  
        default:  
        students = students.OrderBy(stu => stu.CandidateName);  
        break;  
    }  
    return View(students.ToList()); 
   

        // using (var context = new CandidateDBContext())
        // {
        //     var candidateList = context.CandidateInformations.ToList();
        //     return View(candidateList);
        // }
    }

    [HttpPost]
    public IActionResult AddCandidate(Candidate candidate,IFormFile Image,IFormFile Resume)
    {
     string wwwPath = this.Environment.WebRootPath;
        string contentPath = this.Environment.ContentRootPath;
 
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
 
        List<string> uploadedFiles = new List<string>();
       
            string fileName = Path.GetFileName(Image.FileName);
                  using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                Image.CopyTo(stream);
                uploadedFiles.Add(fileName);
                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            }


            //Resume
           wwwPath = this.Environment.WebRootPath;
         contentPath = this.Environment.ContentRootPath;
 
       path = Path.Combine(this.Environment.WebRootPath, "Upload_Resume");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
 
        
       
            string resumefile = Path.GetFileName(Resume.FileName);
                  using (FileStream stream = new FileStream(Path.Combine(path, resumefile), FileMode.Create))
            {
                Resume.CopyTo(stream);
                uploadedFiles.Add(resumefile);
                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", resumefile);
            }
            


        using (var context = new CandidateDBContext())
        {
            CandidateInformation candidateInformation = new CandidateInformation();
            candidateInformation.CandidateName = candidate.CandidateName;
            candidateInformation.Image = fileName;
            candidateInformation.Dob = candidate.Dob;
            candidateInformation.Address = candidate.Address;
            candidateInformation.Mobile = candidate.Mobile;
            candidateInformation.Email = candidate.Email;
            candidateInformation.Technology = candidate.Technology;
            candidateInformation.Resume = resumefile;
            candidateInformation.Description = candidate.Description;
            if (candidate.Id > 0)
            {
                candidateInformation.Id = candidate.Id;
                context.Update(candidateInformation);
            }
            else
            {
                context.Add(candidateInformation);
            }

            context.SaveChanges();

        }

        return RedirectToAction(actionName: "candidate_view", controllerName: "Home");
        // return View();
    }
    public IActionResult DeleteCandidate(int Id)
    {
        using (var context = new CandidateDBContext())
        {
            var candidateRecord = context.CandidateInformations.FirstOrDefault(x => x.Id == Id);
            if (candidateRecord != null)
            {
                context.CandidateInformations.Remove(candidateRecord);
                context.SaveChanges();

            }
            return RedirectToAction(actionName: "candidate_view", controllerName: "Home");
        }
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
