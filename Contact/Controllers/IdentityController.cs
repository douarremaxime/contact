using Contact.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contact.Controllers
{
    /// <summary>
    /// Identity controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Registration request.</param>
        /// <param name="userManager">User manager.</param>
        /// <returns>No content.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterAsync(
            RegisterRequest request,
            UserManager<IdentityUser<long>> userManager)
        {
            var user = new IdentityUser<long>(request.Username);

            var result = await userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return NoContent();
        }

        private ActionResult CreateValidationProblem(IdentityResult result)
        {
            var errorDictionary = new ModelStateDictionary();

            foreach (var error in result.Errors)
            {
                errorDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }

            return ValidationProblem(errorDictionary);
        }
    }
}
