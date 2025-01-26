using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            var data = db.Registrations.ToList();

            var model = new RegistrationViewModel
            {
                Registrations = data.Select(r => new Registration
                {
                    Name = r.Name,
                    PhoneNo = r.PhoneNo,
                    Email = r.Email,
                    Gender = r.Gender,
                    IsMvcSkill = r.IsMvcSkill ?? "No",  // Default to "No" if null
                    IsCSharpSkill = r.IsCSharpSkill ?? "No",
                    IsAspNetSkill = r.IsAspNetSkill ?? "No",
                    Address = r.Address
                }).ToList()
            };

            // Ensure at least one registration is available
            if (!model.Registrations.Any())
            {
                model.Registrations = new List<Registration>
        {
            new Registration()
        };
            }

            return View(model);
        }





        [HttpPost]
        public ActionResult Register(Registration model)
        {
            if (ModelState.IsValid)
            {
                // Process the data (e.g., save to database or display success message)
                ViewBag.Message = "Form submitted successfully!";
                return View("Register"); // Redirect to a success page
            }

            // If validation fails, return the same view
            return View(model);
        }
        [HttpPost]
        public JsonResult SubmitForm(RegistrationViewModel model)
        {
            try
            {
                // Clear the existing data
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE Registrations");

                if (model.Registrations != null && model.Registrations.Any())
                {
                    foreach (var registration in model.Registrations)
                    {
                        // Validate fields for each registration
                        if (string.IsNullOrWhiteSpace(registration.Name) ||
                            string.IsNullOrWhiteSpace(registration.PhoneNo) ||
                            string.IsNullOrWhiteSpace(registration.Email) ||
                            string.IsNullOrWhiteSpace(registration.Gender) ||
                            (string.IsNullOrEmpty(registration.IsMvcSkill) &&
                             string.IsNullOrEmpty(registration.IsCSharpSkill) &&
                             string.IsNullOrEmpty(registration.IsAspNetSkill)))
                        {
                            return Json(new
                            {
                                success = false,
                                message = $"All fields are required for registration: {registration.Name ?? "Unknown"}."
                            });
                        }

                        // Add to the database
                        db.Registrations.Add(new Registration
                        {
                            Name = registration.Name,
                            PhoneNo = registration.PhoneNo,
                            Email = registration.Email,
                            Gender = registration.Gender,
                            IsMvcSkill = registration.IsMvcSkill ?? "No",
                            IsCSharpSkill = registration.IsCSharpSkill ?? "No",
                            IsAspNetSkill = registration.IsAspNetSkill ?? "No",
                            Address = registration.Address
                        });
                    }

                    db.SaveChanges();

                    // Success response
                    return Json(new
                    {
                        success = true,
                        message = "Registrations saved successfully!"
                    });
                }

                // If no registrations are provided
                return Json(new
                {
                    success = false,
                    message = "No registrations were submitted."
                });
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return Json(new
                {
                    success = false,
                    message = "An error occurred while saving the data. Please try again later.",
                    error = ex.Message
                });
            }
        }

    }
}