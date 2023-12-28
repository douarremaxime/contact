using Microsoft.AspNetCore.Identity;
using Npgsql;
using NpgsqlTypes;

namespace Contact.Stores
{
    /// <summary>
    /// User store.
    /// </summary>
    public sealed class UserStore :
        IUserPasswordStore<IdentityUser<long>>,
        IUserSecurityStampStore<IdentityUser<long>>
    {
        /// <summary>
        /// Data source.
        /// </summary>
        private readonly NpgsqlDataSource _dataSource;

        /// <summary>
        /// true if Dispose() was called, false otherwise.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataSource">Data source.</param>
        public UserStore(NpgsqlDataSource dataSource) =>
            _dataSource = dataSource;

        #region IUserStore
        /// <inheritdoc/>
        public Task<string> GetUserIdAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            return Task.FromResult(user.Id.ToString());
        }

        /// <inheritdoc/>
        public Task<string?> GetUserNameAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            return Task.FromResult(user.UserName);
        }

        /// <inheritdoc/>
        public Task SetUserNameAsync(
            IdentityUser<long> user,
            string? userName,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            user.UserName = userName;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<string?> GetNormalizedUserNameAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            return Task.FromResult(user.NormalizedUserName);
        }

        /// <inheritdoc/>
        public Task SetNormalizedUserNameAsync(
            IdentityUser<long> user,
            string? normalizedName,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> CreateAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO users VALUES (DEFAULT, ($1), ($2), ($3), ($4))";

            var userNameParam = new NpgsqlParameter
            {
                Value = user.UserName,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            var normalizedUserNameParam = new NpgsqlParameter
            {
                Value = user.NormalizedUserName,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            var passwordHashParam = new NpgsqlParameter
            {
                Value = user.PasswordHash,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            var securityStampParam = new NpgsqlParameter
            {
                Value = user.SecurityStamp,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters =
                {
                    userNameParam,
                    normalizedUserNameParam,
                    passwordHashParam,
                    securityStampParam
                }
            };

            await command.ExecuteNonQueryAsync(cancellationToken);

            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> UpdateAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            var sql = "UPDATE users " +
                "SET " +
                    "username = ($1), " +
                    "normalized_username = ($2), " +
                    "password_hash = ($3), " +
                    "security_stamp = ($4) " +
                "WHERE id = ($5)";

            var userNameParam = new NpgsqlParameter
            {
                Value = user.UserName,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            var normalizedUserNameParam = new NpgsqlParameter
            {
                Value = user.NormalizedUserName,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            var passwordHashParam = new NpgsqlParameter
            {
                Value = user.PasswordHash,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            var securityStampParam = new NpgsqlParameter
            {
                Value = user.SecurityStamp,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            var idParam = new NpgsqlParameter<long>
            {
                TypedValue = user.Id,
                NpgsqlDbType = NpgsqlDbType.Bigint
            };

            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters =
                {
                    userNameParam,
                    normalizedUserNameParam,
                    passwordHashParam,
                    securityStampParam,
                    idParam
                }
            };

            var rows = await command.ExecuteNonQueryAsync(cancellationToken);

            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> DeleteAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            var sql = "DELETE FROM users WHERE id = ($1)";

            var idParam = new NpgsqlParameter<long>
            {
                TypedValue = user.Id,
                NpgsqlDbType = NpgsqlDbType.Bigint
            };

            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters = { idParam }
            };

            var rows = await command.ExecuteNonQueryAsync(cancellationToken);

            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<IdentityUser<long>?> FindByIdAsync(
            string userId,
            CancellationToken cancellationToken)
        {
            var parsedUserId = long.Parse(userId);

            var sql = "SELECT " +
                    "username, " +
                    "normalized_username, " +
                    "password_hash, " +
                    "security_stamp " +
                "FROM users WHERE id = ($1)";

            var idParam = new NpgsqlParameter<long>
            {
                NpgsqlDbType = NpgsqlDbType.Bigint
            };

            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters = { idParam }
            };

            await command.PrepareAsync(cancellationToken);

            idParam.TypedValue = parsedUserId;

            await using var reader =
                await command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
            {
                return new IdentityUser<long>
                {
                    Id = parsedUserId,
                    UserName = reader.GetString(0),
                    NormalizedUserName = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                    SecurityStamp = reader.GetString(3)
                };
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<IdentityUser<long>?> FindByNameAsync(
            string normalizedUserName,
            CancellationToken cancellationToken)
        {
            var sql = "SELECT " +
                    "id, " +
                    "username, " +
                    "password_hash, " +
                    "security_stamp " +
                "FROM users WHERE normalized_username = ($1)";

            var normalizedUserNameParam = new NpgsqlParameter
            {
                Value = normalizedUserName,
                NpgsqlDbType = NpgsqlDbType.Varchar
            };

            await using var connection =
                await _dataSource.OpenConnectionAsync(cancellationToken);

            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters = { normalizedUserNameParam }
            };

            await using var reader =
                await command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
            {
                return new IdentityUser<long>
                {
                    Id = reader.GetInt64(0),
                    UserName = reader.GetString(1),
                    NormalizedUserName = normalizedUserName,
                    PasswordHash = reader.GetString(2),
                    SecurityStamp = reader.GetString(3)
                };
            }

            return null;
        }
        #endregion

        #region IUserPasswordStore
        /// <inheritdoc/>
        public Task SetPasswordHashAsync(
            IdentityUser<long> user,
            string? passwordHash,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<string?> GetPasswordHashAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            return Task.FromResult(user.PasswordHash);
        }

        /// <inheritdoc/>
        public Task<bool> HasPasswordAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            return Task.FromResult(user.PasswordHash is not null);
        }
        #endregion

        #region IUserSecurityStampStore
        /// <inheritdoc/>
        public Task SetSecurityStampAsync(
            IdentityUser<long> user,
            string stamp,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<string?> GetSecurityStampAsync(
            IdentityUser<long> user,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);
            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// Dispose the store.
        /// </summary>
        public void Dispose() => _disposed = true;
        #endregion
    }
}
