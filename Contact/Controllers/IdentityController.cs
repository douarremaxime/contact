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
        /// <param name="request">Sign up request.</param>
        /// <param name="userManager">User manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult> SignUpAsync(
            SignUpRequest request,
            UserManager<IdentityUser<long>> userManager)
        {
            var user = new IdentityUser<long>(request.Username);

            var result = await userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
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

            return NoContent();
        }

        /// <summary>
        /// User sign in.
        /// </summary>
        /// <param name="request">Sign in request.</param>
        /// <param name="signInManager">Sign in manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult> SignInAsync(
            SignInRequest request,
            SignInManager<IdentityUser<long>> signInManager)
        {
            signInManager.AuthenticationScheme =
                IdentityConstants.ApplicationScheme;

            var result = await signInManager.PasswordSignInAsync(
                request.Username,
                request.Password,
                request.IsPersistent,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized(result);

            return NoContent();
        }

        /// <summary>
        /// User sign out.
        /// </summary>
        /// <param name="signInManager">Sign in manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("signout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SignOutAsync(
            SignInManager<IdentityUser<long>> signInManager)
        {
            signInManager.AuthenticationScheme =
                IdentityConstants.ApplicationScheme;

            await signInManager.SignOutAsync();

            return NoContent();
        }
    }
}
