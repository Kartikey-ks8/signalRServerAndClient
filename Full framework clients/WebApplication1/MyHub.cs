using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class MyHub : Hub
    {
        public void SendMessage(string user, string message)
        {
            
            Clients.All.ReceiveMessage(user, message);
        }
    }
}
