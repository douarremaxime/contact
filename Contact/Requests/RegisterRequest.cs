using Contact.Controllers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to register a new user via <see cref="IdentityController.RegisterAsync(RegisterRequest, UserManager{IdentityUser{long}}, IUserStore{IdentityUser{long}})"/>
    /// </summary>
    /// <param name="UserName">UserName.</param>
    /// <param name="Password">Password.</param>
    public record class RegisterRequest(
        [Required] string UserName,
        [Required] string Password);
}