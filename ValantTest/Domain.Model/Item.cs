namespace ValantTest.Domain.Model
{
    using System;

    public class Item : Entity<string>
    {
        public Guid Id { get; set; }

        public string Label
        {
            get
            {
                return this.Key;
            }

            set
            {
                this.Key = value;
            }
        } 

        public string Description { get; set; }

        public string Type { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Count { get; set; }
    }
}
