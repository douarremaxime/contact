namespace Contact.Results
{
    /// <summary>
    /// Represents the result of a sign in operation.
    /// </summary>
    public class SignInResult : Microsoft.AspNetCore.Identity.SignInResult
    {
        private static readonly SignInResult _userNotFound = new() { UserExists = false };

        /// <summary>
        /// True if the user exists, false otherwise.
        /// </summary>
        public bool UserExists { get; protected set; } = true;

        /// <summary>
        /// Returns a <see cref="SignInResult"/> that represents a sign in attempt that failed because
        /// the user does not exist.
        /// </summary>
        public static SignInResult UserNotFound => _userNotFound;
    }
}
