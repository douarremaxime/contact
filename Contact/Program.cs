var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddContactIdentity();
builder.Services.AddContactStores(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var options = new DefaultFilesOptions();
options.DefaultFileNames[0] = "chats.html";
app.UseDefaultFiles(options);

app.UseStaticFiles();

app.MapControllers();

app.Run();
