

using ContactListApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureWebApi();
builder.ConfigureServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseCors();
app.MapControllers();

app.Run();

//dotnet run --launch-profile https 
