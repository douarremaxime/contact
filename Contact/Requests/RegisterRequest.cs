using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to register a new user.
    /// </summary>
    public record class RegisterRequest
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