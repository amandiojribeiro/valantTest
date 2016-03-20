namespace ValantTest.Application.DTO
{
    using System;

    /// <summary>
    /// I implemented a object with the same properties that our Model Object has, only for simplicity reasons
    /// </summary>
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

        public DateTime NotificationDate { get; set; }
    }
}
