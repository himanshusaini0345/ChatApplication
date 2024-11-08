using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string,UserRoomConnection> _connections;

        public ChatHub(IDictionary<string, UserRoomConnection> connections)
        {
            _connections = connections;
        }

        public async Task JoinRoom(UserRoomConnection userRoomConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userRoomConnection.Room);
            _connections[Context.ConnectionId] = userRoomConnection;
            await Clients.Group(userRoomConnection.Room)
                         .SendAsync("RecieveMessage", "Lets Program Bot", $"{userRoomConnection.User} has joined the room.");

        }

        public async Task Sendmessage(string message)
        {
            if(_connections.TryGetValue(Context.ConnectionId, out UserRoomConnection? userRoomConnection))
            {
                await Clients.Group(userRoomConnection.Room)
                             .SendAsync("RecieveMessage", userRoomConnection.User, message, DateTime.Now);
            }
        }

        public async Task SendConnectedUser(string room)
        {
            var users = _connections.Values
                                    .Where(urc => urc.Room == room)
                                    .Select(x => x.User);

            await Clients.Group(room)
                          .SendAsync("ConnectedUser", users);

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if(!_connections.TryGetValue(Context.ConnectionId,out UserRoomConnection? userRoomConnection))
            {
                await base.OnDisconnectedAsync(exception);
            }

            await Clients.Group(userRoomConnection.Room)
                         .SendAsync("RecieveMessage", "Lets Program Bot", $"{userRoomConnection.User} has left the group");
            await SendConnectedUser(userRoomConnection.Room);
        }
    }
}
