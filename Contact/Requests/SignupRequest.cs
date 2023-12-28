using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to sign up a new user.
    /// </summary>
    public record class SignUpRequest
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
    }
}