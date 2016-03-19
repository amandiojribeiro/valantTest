namespace ValantTest.Domain.Core.TypedGateways
{
    using System.Diagnostics.CodeAnalysis;
    using GatewayInterfaces;
    using Model;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public interface ISignalRGateway : IGateway<Notification>
    {
    }
}
