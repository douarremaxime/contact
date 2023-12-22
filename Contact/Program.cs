using Contact.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddIdentityApiEndpoints<IdentityUser<long>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
});

builder.Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build());

var connectionString = builder.Configuration.GetConnectionString("Npgsql") 
    ?? throw new ArgumentException("Missing Npgsql connection string.");

builder.Services.AddNpgsqlDataSource(connectionString);

builder.Services.AddScoped<IUserStore<IdentityUser<long>>, UserStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
    app.UseStatusCodePages();
    app.UseHsts();
}

app.UseHttpsRedirection();

var options = new DefaultFilesOptions();
options.DefaultFileNames[0] = "signin.html";
app.UseDefaultFiles(options);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
