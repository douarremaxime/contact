var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddContactIdentity();

builder.Services.AddContactStores(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
