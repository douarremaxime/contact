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
        /// <param name="request">Signup request.</param>
        /// <param name="userManager">User manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterAsync(
            SignupRequest request,
            UserManager<IdentityUser<long>> userManager)
        {
            var user = new IdentityUser<long>(request.Username);

            var result = await userManager.CreateAsync(
                user,
                request.Password);

            if (result.Succeeded)
                return NoContent();

            return ValidationProblem(
                result.Errors.Aggregate(
                    seed: new ModelStateDictionary(),
                    func: (errorDictionary, error) =>
                    {
                        errorDictionary.AddModelError(
                            error.Code,
                            error.Description);

                        return errorDictionary;
                    }));
        }
    }
}
