using GorodSO.Database;
using GorodSO.Models;
using GorodSO.Services;
using GorodSO.Services.MessagesServices;
using VkNet;
using VkNet.Enums.StringEnums;
using VkNet.Model;
using Button = VkNet.Model.Button;

namespace GorodSO.BackgroundServices;

public class MessagesBackgroundService(VkApi vkApi, GlobalMessagesService globalMessagesService) : BackgroundService
{
    private ulong _groupId = 237188573;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var server = vkApi.Groups.GetLongPollServer(_groupId);
        
        while (true)
        {
            try
            {
                var poll = vkApi.Groups.GetBotsLongPollHistory(
                    new BotsLongPollHistoryParams
                    {
                        Server = server.Server,
                        Ts = server.Ts,
                        Key = server.Key,
                        Wait = 25
                    });
                
                server.Ts = poll.Ts;
                
                if (poll?.Updates == null) continue;
                
                foreach (var update in poll.Updates)
                {
                    ProcessUpdate(update);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // При ошибке получаем новый сервер
                server = vkApi.Groups.GetLongPollServer(_groupId);
            }
        }
    }

    private void ProcessUpdate(GroupUpdate update)
    {
        if (update.Type.Value.Value == GroupUpdateType.MessageNew)
        {
            var messageUpdate = ((MessageNew)update.Instance);
            Console.WriteLine($"Got message {messageUpdate.Message.Text} from {messageUpdate.Message.PeerId.Value}");
            var answer = globalMessagesService.Handle((int) messageUpdate.Message.PeerId.Value, messageUpdate.Message.Text);
            vkApi.Messages.Send(new MessagesSendParams()
            {
                Message = answer,
                PeerId = messageUpdate.Message.PeerId,
                RandomId = new Random().Next(1, int.MaxValue),
                Keyboard = new MessageKeyboard()
                {
                    Buttons = new IEnumerable<MessageKeyboardButton>[]{new MessageKeyboardButton[]
                    {
                        new MessageKeyboardButton()
                        {
                            Action = new MessageKeyboardButtonAction()
                            {
                                Type = KeyboardButtonActionType.Text,
                                Label = "/help"
                            }
                        }
                    }}
                }
            });
        }
    }
}