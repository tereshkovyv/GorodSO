using GorodSO.BackgroundServices;
using GorodSO.Services;
using GorodSO.Services.MessagesServices;
using VkNet;
using VkNet.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<VkApi>(sp => {
    var api = new VkApi();
    api.Authorize(new ApiAuthParams{ AccessToken = builder.Configuration["Config:AccessToken"] });
    return api;
});
builder.Services.AddControllers();
builder.Services.AddSingleton<MessagesBackgroundService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<MessagesBackgroundService>());
builder.Services.AddSingleton<GlobalMessagesService>();
builder.Services.AddSingleton<CategoriesService>();
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<AdminMessagesService>();
builder.Services.AddSingleton<CoordinatorMessagesService>();
builder.Services.AddSingleton<ModeratorMessagesService>();
builder.Services.AddSingleton<ParticipantMessagesService>();
builder.Services.AddSingleton<CompetitionsService>();
builder.Services.AddSingleton<TasksService>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();
