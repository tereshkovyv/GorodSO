using System.Text.Json;
using GorodSO.Models;
using Microsoft.AspNetCore.Mvc;
using VkNet.Abstractions;
using VkNet.Model;

namespace GorodSO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallbackController(IConfiguration configuration, IVkApi vkApi) : ControllerBase
{
    [HttpPost]
    public IActionResult Callback([FromBody] Updates updates)
    {
        switch (updates.Type)
        {
            case "confirmation":
                return Ok(configuration["Config:Confirmation"]);
            
            case "message_new":
            {
                var msg = updates.Object.Deserialize<Message>();

                vkApi.Messages.Send(new MessagesSendParams{ 
                    RandomId = new DateTime().Millisecond,
                    PeerId = msg.PeerId.Value,
                    Message = msg.Text
                });
                break;
            }
        }
        return Ok("ok");
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Hello world");
    }
}