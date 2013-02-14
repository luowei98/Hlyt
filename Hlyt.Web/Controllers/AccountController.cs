using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hlyt.CommandProcessor.Dispatcher;
using Hlyt.Core.Common;
using Hlyt.Data.Repositories;
using Hlyt.Domain.Commands.Security;
using Hlyt.Model.Security;
using Hlyt.Web.Core.Authentication;
using Hlyt.Web.Core.Extensions;
using Hlyt.Web.Core.Models;
using Hlyt.Web.ViewModels;


namespace Hlyt.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IFormsAuthentication formAuthentication;
        private readonly IUserRepository userRepository;

        public AccountController(ICommandBus commandBus, IUserRepository userRepository,
                                 IFormsAuthentication formAuthentication)
        {
            this.commandBus = commandBus;
            this.userRepository = userRepository;
            this.formAuthentication = formAuthentication;
        }

        public ActionResult LogOff()
        {
            formAuthentication.Signout();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return ContextDependentView();
        }

        private bool ValidatePassword(User user, string password)
        {
            var encoded = Md5Encrypt.Md5EncryptPassword(password);
            return user.PasswordHash.Equals(encoded);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return ContextDependentView();
        }

        [HttpPost]
        public ActionResult Login(LogOnFormModel form, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = userRepository.Get(u => u.Email == form.UserName && u.Activated);
                if (user != null)
                {
                    if (ValidatePassword(user, form.Password))
                    {
                        formAuthentication.SetAuthCookie(HttpContext,
                                                         UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                             user));

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed
            return View("Login", form);
        }

        [HttpPost]
        public JsonResult JsonLogin(LogOnFormModel form, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = userRepository.Get(u => u.Email == form.UserName && u.Activated);
                if (user != null)
                {
                    if (ValidatePassword(user, form.Password))
                    {
                        formAuthentication.SetAuthCookie(HttpContext,
                                                         UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                             user));

                        return Json(new {success = true, redirect = returnUrl});
                    }

                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed
            return Json(new {errors = GetErrorsFromModelState(), success = false});
        }

        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(UserFormModel form)
        {
            if (ModelState.IsValid)
            {
                var command = Mapper.Map<UserFormModel, UserRegisterCommand>(form);
                command.Activated = true;
                command.RoleId = (Int32) UserRoles.User;
                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success)
                    {
                        User user = userRepository.Get(u => u.Email == form.Email);
                        formAuthentication.SetAuthCookie(HttpContext,
                                                         UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                             user));
                        return Json(new {success = true});
                    }

                    ModelState.AddModelError("", "An unknown error occurred.");
                }
                // If we got this far, something failed
                return Json(new {errors = GetErrorsFromModelState()});
            }

            // If we got this far, something failed
            return Json(new {errors = GetErrorsFromModelState()});
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UserFormModel form)
        {
            if (ModelState.IsValid)
            {
                var command = Mapper.Map<UserFormModel, UserRegisterCommand>(form);
                command.Activated = true;
                command.RoleId = (Int32) UserRoles.User;
                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success)
                    {
                        User user = userRepository.Get(u => u.Email == form.Email);
                        formAuthentication.SetAuthCookie(HttpContext,
                                                         UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                             user));
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "An unknown error occurred.");
                }
                // If we got this far, something failed, redisplay form
                return View(form);
            }

            // If we got this far, something failed
            return Json(new {errors = GetErrorsFromModelState()});
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }

            ViewBag.FormAction = actionName;
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordFormModel form)
        {
            if (ModelState.IsValid)
            {
                HlytUser yiheUser = HttpContext.User.GetHlytUser();
                var command = new ChangePasswordCommand
                              {
                                  UserId = yiheUser.UserId,
                                  OldPassword = form.OldPassword,
                                  NewPassword = form.NewPassword
                              };
                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success)
                    {
                        return RedirectToAction("ChangePasswordSuccess");
                    }

                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(form);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
