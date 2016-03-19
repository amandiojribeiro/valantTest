namespace ValantTest.Infrastructure.CrossCutting.Adapters.Automapper
{
    public interface ITypeAdapterFactory
    {
        /// <summary>
        /// Create a type adapter
        /// </summary>
        /// <returns>The created ITypeAdapter</returns>
        ITypeAdapter Create();
    }
}
