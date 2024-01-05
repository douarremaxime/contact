using Contact.Exceptions;
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
        /// Changes the password of a user.
        /// </summary>
        /// <param name="request">Change password request.</param>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sign in manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("change-password")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult> ChangePasswordAsync(
            [FromForm] ChangePasswordRequest request,
            [FromServices] UserManager<IdentityUser<long>> userManager,
            [FromServices] SignInManager<IdentityUser<long>> signInManager)
        {
            var user = await userManager.GetUserAsync(HttpContext.User)
                ?? throw new UserNotFoundException();

            var result = await userManager.ChangePasswordAsync(
                user,
                request.CurrentPassword,
                request.NewPassword);

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

            await signInManager.RefreshSignInAsync(user);

            return NoContent();
        }

        /// <summary>
        /// Changes the username of a user.
        /// </summary>
        /// <param name="request">Change username request.</param>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sign in manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("change-username")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult> ChangeUsernameAsync(
            [FromForm] ChangeUsernameRequest request,
            [FromServices] UserManager<IdentityUser<long>> userManager,
            [FromServices] SignInManager<IdentityUser<long>> signInManager)
        {
            var user = await userManager.GetUserAsync(HttpContext.User)
                ?? throw new UserNotFoundException();

            var result = await userManager.SetUserNameAsync(
                user,
                request.NewUsername);

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

            await signInManager.RefreshSignInAsync(user);

            return NoContent();
        }

        /// <summary>
        /// User sign in.
        /// </summary>
        /// <param name="request">Sign in request.</param>
        /// <param name="signInManager">Sign in manager.</param>
        /// <returns>An action result.</returns>
        [AllowAnonymous]
        [HttpPost("signin")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult> SignInAsync(
            [FromForm] SignInRequest request,
            [FromServices] SignInManager<IdentityUser<long>> signInManager)
        {
            if (!request.IsPersistent)
                signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync(
                request.Username,
                request.Password,
                request.IsPersistent,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized();

            // The signInManager already produced the needed response
            // in the form of a cookie or bearer token.
            return Empty;
        }

        /// <summary>
        /// Sign out all devices by updating the user security stamp.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sign in manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("signout-all")]
        public async Task<ActionResult> SignOutAllDevicesAsync(
            [FromServices] UserManager<IdentityUser<long>> userManager,
            [FromServices] SignInManager<IdentityUser<long>> signInManager)
        {
            var user = await userManager.GetUserAsync(HttpContext.User)
                ?? throw new UserNotFoundException();

            var result = await userManager.UpdateSecurityStampAsync(user);

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

            await signInManager.SignOutAsync();

            return NoContent();
        }

        /// <summary>
        /// User sign out.
        /// </summary>
        /// <param name="signInManager">Sign in manager.</param>
        /// <returns>An action result.</returns>
        [HttpPost("signout")]
        public async Task<ActionResult> SignOutAsync(
            [FromServices] SignInManager<IdentityUser<long>> signInManager)
        {
            await signInManager.SignOutAsync();

            return NoContent();
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Sign up request.</param>
        /// <param name="userManager">User manager.</param>
        /// <returns>An action result.</returns>
        [AllowAnonymous]
        [HttpPost("signup")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult> SignUpAsync(
            [FromForm] SignUpRequest request,
            [FromServices] UserManager<IdentityUser<long>> userManager)
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
    }
}
