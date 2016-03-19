namespace ValantTest.Presentation.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Application.DTO;
    using Application.Services;

    [RoutePrefix("notifications")]
    public class NotificationsController : ApiController
    {
        private readonly INotificationService notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Get Notifications by date.
        /// </summary>
        /// <param name="date">The date of the notification.</param>
        /// <returns>The Outfit.</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet, Route("{date}"), ResponseType(typeof(List<NotificationDto>))]
        public async Task<HttpResponseMessage> GetNotificationsByDate(DateTime date)
        {
            var result = await this.notificationService.GetNotificationsByDate(date);
            if (result != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
        }
    }
}