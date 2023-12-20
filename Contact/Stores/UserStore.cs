using Microsoft.AspNetCore.Identity;
using Npgsql;

namespace Contact.Stores
{
    /// <summary>
    /// Implements a <see cref="IUserPasswordStore{TUser}"/> 
    /// for <see cref="IdentityUser{TKey}"/>.
    /// </summary>
    public class UserStore : IUserPasswordStore<IdentityUser<int>>
    {
        /// <summary>
        /// Data source.
        /// </summary>
        private readonly NpgsqlDataSource _dataSource;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataSource">Data source.</param>
        public UserStore(NpgsqlDataSource dataSource) =>
            _dataSource = dataSource;

        #region IUserStore
        /// <inheritdoc/>
        public async Task<string> GetUserIdAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            var sql = "SELECT id FROM users WHERE normalized_username = ($1)";

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters =
                {
                    new() { Value = user.NormalizedUserName }
                }
            };

            await using var reader =
                await command.ExecuteReaderAsync(cancellationToken);

            await reader.ReadAsync(cancellationToken);

            return reader.GetString(0);
        }

        /// <inheritdoc/>
        public async Task<string?> GetUserNameAsync(
            IdentityUser<int> user,
            CancellationToken cancellationToken)
        {
            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            var sql = "SELECT username FROM users WHERE id = ($1)";

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters =
                {
                    new() { Value = user.Id }
                }
            };

            await using var reader =
                await command.ExecuteReaderAsync(cancellationToken);

            await reader.ReadAsync(cancellationToken);

            return reader.GetString(0);
        }

        /// <inheritdoc/>
        public async Task SetUserNameAsync(
            IdentityUser<int> user,
            string? userName,
            CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(userName);

            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            var sql = "UPDATE users SET username = ($1) WHERE id = ($2)";

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters =
                {
                    new() { Value = userName },
                    new() { Value = user.Id }
                }
            };

            await command.ExecuteNonQueryAsync(cancellationToken);
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
