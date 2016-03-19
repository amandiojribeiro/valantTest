namespace ValantTest.Domain.Model
{
    using System;

    public class Item
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Count { get; set; }
    }
}
