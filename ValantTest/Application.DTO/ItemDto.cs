namespace ValantTest.Application.DTO
{
    using System;

    /// <summary>
    /// I implemented a object with the same properties that our Model Object has only, for simplicity reasons
    /// </summary>
    public class ItemDto
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Count { get; set; }
    }
}
