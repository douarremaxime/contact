using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to change a username.
    /// </summary>
    public record class ChangeUsernameRequest
    {
        /// <summary>
        /// New username.
        /// </summary>
        [Required]
        public required string NewUsername { get; init; }
    }
}
