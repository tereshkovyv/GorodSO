using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IVkApi>(sp => {
    var api = new VkApi();
    api.Authorize(new ApiAuthParams{ AccessToken = builder.Configuration["Config:AccessToken"] });
    return api;
});
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();