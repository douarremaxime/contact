namespace Contact.Exceptions
{
    /// <summary>
    /// Exception thrown when a user was not found.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UserNotFoundException"/>.
        /// </summary>
        public UserNotFoundException()
        {
        }

        /// <inheritdoc/>
        public UserNotFoundException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
