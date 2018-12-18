using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Profile.BLL.DTO;
using Profile.BLL.Infrastructure;
using Profile.BLL.Interfaces;
using ProfileApp.AdditionalClasses;
using ProfileApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProfileApp.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            { 
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Surname = model.Surname,
                    Role = "user"
                };

                string path = String.Empty;
                if (model.Photo != null && model.Photo.ContentLength > 0)
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(model.Photo.FileName));
                        model.Photo.SaveAs(path);
                        userDto.Photo = model.Photo.FileName;
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }


                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View(viewName:"SuccessRegister",model:Mapper.FromUserToProfile(userDto));
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    if(String.IsNullOrEmpty(path)==false)
                    System.IO.File.Delete(path);
                }
            }
            return View(model);
        }
        
        public async Task<ActionResult> IsUniqueEmail(string Email)
        {
            bool isUnique =await UserService.IsUserByEmail(Email);
            return PartialView(viewName: "EmailCheck", model: isUnique);
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Name = "Семен Семенович Горбунков",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}