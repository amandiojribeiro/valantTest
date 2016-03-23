namespace ValantTest.Domain.Model
{
    public abstract class Entity<TId>
    {
        public TId Key { get; set; }
    }
}
