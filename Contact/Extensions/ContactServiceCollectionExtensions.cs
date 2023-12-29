﻿using Contact.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up Contact services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ContactServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Identity services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>An <see cref="IServiceCollection"/> that can be used to further configure services.</returns>
        public static IServiceCollection AddContactIdentity(this IServiceCollection services)
        {
            services.AddIdentityApiEndpoints<IdentityUser<long>>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.Lockout.AllowedForNewUsers = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/access-denied.html";
                options.Cookie.Name = "contact_auth";
                options.LoginPath = "/signin.html";
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(1);
            });

            services.AddAuthorizationBuilder()
                .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build());

            return services;
        }

        /// <summary>
        /// Adds storage services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>An <see cref="IServiceCollection"/> that can be used to further configure services.</returns>
        /// <exception cref="ArgumentException">Connection string is null or missing.</exception>
        public static IServiceCollection AddContactStores(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Npgsql")
                ?? throw new ArgumentException("Null or missing Npgsql connection string.");

            services.AddNpgsqlDataSource(connectionString);

            services.AddScoped<IUserStore<IdentityUser<long>>, UserStore>();

            return services;
        }
    }
}
