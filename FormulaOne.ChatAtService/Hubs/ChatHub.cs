using FormulaOne.ChatAtService.Models;
using Microsoft.AspNetCore.SignalR;

namespace FormulaOne.ChatAtService.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveMessage($"{Context.ConnectionId}");
    }

    public async Task JoinChat(UserConnection conn)
    {
        await Clients.All.ReceiveMessage($"{Context.ConnectionId} just join the chat" );
    }

    public async Task SendChadMessageWithConId(string connectionId,string Message)
    {
        await Clients.Client(connectionId).ReceiveMessage(message: Message);
    }
}

