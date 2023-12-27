using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to register a new user.
    /// </summary>
    public record class SignupRequest
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
    }
}