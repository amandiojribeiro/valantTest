namespace ValantTest.Domain.Model
{
    using System;

    public class Notification : Entity<Guid>
    {
        public Guid Id
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

        public int Type { get; set; }

        public string Description { get; set; }

        public DateTime NotificationDate { get; set; }
    }
}
