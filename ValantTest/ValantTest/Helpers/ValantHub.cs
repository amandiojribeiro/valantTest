namespace ValantTest.Presentation.Api.Helpers
{
    using System.Diagnostics;
    using Microsoft.AspNet.SignalR;

    public class ValantHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.sendMessage(message);
            Debug.Write(message);
        }
    }
}