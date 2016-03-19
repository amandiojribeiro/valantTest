namespace ValantTest.Presentation.Api.Helpers
{
    using Microsoft.AspNet.SignalR;
    using System.Diagnostics;

    public class ValantHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.sendMessage(message);
            Debug.Write(message);
        }
    }
}