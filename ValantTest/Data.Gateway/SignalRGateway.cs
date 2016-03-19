namespace ValantTest.Data.Gateway
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Domain.Core.TypedGateways;
    using Infrastructure.CrossCutting.Configuration;
    using Microsoft.AspNet.SignalR.Client;
    using Newtonsoft.Json;
    using Domain.Model;

    public class SignalRGateway : ISignalRGateway
    {
        private readonly IHubProxy hubProxy;

        private readonly HubConnection connection;

        public SignalRGateway()
        {
            this.connection = new HubConnection(Settings.ServerUri);
            this.hubProxy = connection.CreateHubProxy(Settings.HubName);
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
