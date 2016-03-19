namespace ValantTest.Data.Gateway
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Domain.Core.TypedGateways;
    using Domain.Model;
    using Infrastructure.CrossCutting.Configuration;
    using Microsoft.AspNet.SignalR.Client;
    using Newtonsoft.Json;

    public class SignalRGateway : ISignalRGateway
    {
        private readonly IHubProxy hubProxy;

        private readonly HubConnection connection;

        public SignalRGateway()
        {
            this.connection = new HubConnection(Settings.ServerUri);
            this.hubProxy = this.connection.CreateHubProxy(Settings.HubName);
        }

        public async Task SendMessage(Notification message)
        {
            try
            {
                await this.connection.Start();
                await this.hubProxy.Invoke("SendMessage", JsonConvert.SerializeObject(message));
            }
            catch (HttpRequestException ex)
            {
                Debug.Write(ex, "Error on SignalR Gateway!!!!");
            }
        }
    }
}
