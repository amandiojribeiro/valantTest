namespace ValantTest.Domain.Model
{
    using System;

    public class Notification
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

        public DateTime NotificationDate { get; set; }
    }
}
