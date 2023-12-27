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
    }
}
