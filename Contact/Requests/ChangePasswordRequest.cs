using System.ComponentModel.DataAnnotations;

namespace Contact.Requests
{
    /// <summary>
    /// Request to change a password.
    /// </summary>
    public record class ChangePasswordRequest
    {
        /// <summary>
        /// New password.
        /// </summary>
        [Required]
        public required string NewPassword { get; init; }
    }
}
