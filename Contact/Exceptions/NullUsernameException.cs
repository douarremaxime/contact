namespace Contact.Exceptions
{
    /// <summary>
    /// Exception thrown when a user does not have a username.
    /// </summary>
    public class NullUsernameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullUsernameException"/>.
        /// </summary>
        public NullUsernameException()
        {
        }

        /// <inheritdoc/>
        public NullUsernameException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        public NullUsernameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
