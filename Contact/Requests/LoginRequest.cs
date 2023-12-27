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
        /// Flag indicating whether the sign-in cookie should persist after the browser is closed.
        /// </summary>
        [Required]
        public required bool IsPersistent { get; init; }
    }
}
