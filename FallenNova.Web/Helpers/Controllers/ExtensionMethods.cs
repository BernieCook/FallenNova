using System.Collections.Generic;
using System.Web.Mvc;

namespace FallenNova.Web.Controllers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Extends the ModelState object so we can more easily set the error messages, typically service level errorMessages.  
        /// </summary>
        /// <param name="modelState">Model state.</param>
        /// <param name="errorMessages">Error message collection.</param>
        public static void AddModelErrors(
            this ModelStateDictionary modelState,
            IEnumerable<string> errorMessages)
        {
            foreach (var errorMessage in errorMessages)
            {
                modelState.AddModelError(string.Empty, errorMessage);
            }
        }

        /// <summary>
        /// Extends the ModelState object so we can more easily set a single error message. 
        /// </summary>
        /// <param name="modelState">Model state.</param>
        /// <param name="errorMessage">Error message.</param>
        public static void AddModelError(
            this ModelStateDictionary modelState,
            string errorMessage)
        {
            modelState.AddModelError(string.Empty, errorMessage);
        }
    }
}