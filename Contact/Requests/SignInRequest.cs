using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to sign in.
    /// </summary>
    public record class SignInRequest
    {
        /// <summary>
        /// Username.
        /// </summary>
        [Required]
        public required string Username { get; init; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required, MinLength(6)]
        public required string Password { get; init; }

        /// <summary>
        /// True to persist the sign in cookie after the browser is closed, false otherwise.
        /// </summary>
        [Required]
        public required bool IsPersistent { get; init; }
    }
}
