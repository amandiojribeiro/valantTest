namespace ValantTest.Domain.Core.GatewayInterfaces
{
    using System.Threading.Tasks;

    public interface IGateway<TEntity> where TEntity : class
    {
        Task SendMessage(TEntity message);
    }
}
