﻿using System.Collections.Generic;
using System.Web.Mvc;
using Hlyt.Core.Common;


namespace Hlyt.Web.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddModelErrors(this Controller controller, IEnumerable<ValidationResult> validationResults,
                                          string defaultErrorKey = null)
        {
            if (validationResults == null) return;

            foreach (ValidationResult validationResult in validationResults)
            {
                if (!string.IsNullOrEmpty(validationResult.MemberName))
                {
                    controller.ModelState.AddModelError(validationResult.MemberName, validationResult.Message);
                }
                else if (defaultErrorKey != null)
                {
                    controller.ModelState.AddModelError(defaultErrorKey, validationResult.Message);
                }
                else
                {
                    controller.ModelState.AddModelError(string.Empty, validationResult.Message);
                }
            }
        }

        public static void AddModelErrors(this ModelStateDictionary modelState,
                                          IEnumerable<ValidationResult> validationResults, string defaultErrorKey = null)
        {
            if (validationResults == null) return;

            foreach (ValidationResult validationResult in validationResults)
            {
                string key = validationResult.MemberName ?? defaultErrorKey ?? string.Empty;
                modelState.AddModelError(key, validationResult.Message);
            }
        }
    }
}
