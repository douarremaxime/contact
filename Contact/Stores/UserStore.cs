using Microsoft.AspNetCore.Identity;

namespace Contact.Stores
{
    /// <summary>
    /// Implements a <see cref="IUserPasswordStore{TUser}"/> 
    /// for <see cref="IdentityUser{TKey}"/>.
    /// </summary>
    public class UserStore : IUserPasswordStore<IdentityUser<int>>
    {
        #region IUserStore
        /// <inheritdoc/>
        public Task<string> GetUserIdAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<string?> GetUserNameAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task SetUserNameAsync(
            IdentityUser<int> user,
            string? userName,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<string?> GetNormalizedUserNameAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task SetNormalizedUserNameAsync(
            IdentityUser<int> user,
            string? normalizedName,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IdentityResult> CreateAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IdentityResult> UpdateAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IdentityResult> DeleteAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IdentityUser<int>?> FindByIdAsync(
            string userId,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IdentityUser<int>?> FindByNameAsync(
            string normalizedUserName,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserPasswordStore
        /// <inheritdoc/>
        public Task SetPasswordHashAsync(
            IdentityUser<int> user,
            string? passwordHash,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<string?> GetPasswordHashAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<bool> HasPasswordAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IDisposable
        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
