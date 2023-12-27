using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to log in.
    /// </summary>
    public record class LoginRequest
    {
        /// <summary>
        /// Username.
        /// </summary>
        [Required]
        public required string Username { get; init; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        public required string Password { get; init; }

        /// <summary>
        /// true to use cookie-based authentication, false to use token-based authentication.
        /// </summary>
        [Required]
        public required bool? UseCookies { get; init; }

        /// <summary>
        /// true to use session cookies, false to use persistent cookies.
        /// </summary>
        [Required]
        public required bool? UseSessionCookies { get; init; }
    }
}
